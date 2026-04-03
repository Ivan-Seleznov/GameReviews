using System.Data;
using GameReviews.Application.Common.Interfaces;
using GameReviews.Application.Common.Interfaces.Authentication;
using GameReviews.Domain.Common.Abstractions.Repositories;
using GameReviews.Domain.Common.Abstractions.Services;
using GameReviews.Domain.Entities.RolesAggregate.Entities;
using GameReviews.Domain.Entities.UserAggregate.Entities;
using GameReviews.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameReviews.Web.Extensions;

public static class WebApplicationExtensions
{
    /// <summary>
    /// Seeds an administrator user into the database. 
    /// Intended for development and testing only; better not use in production :)
    /// </summary>
    public static async Task SeedAdminAsync(this IHost app)
    {
        await using var scope = app.Services.CreateAsyncScope();

        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationWriteDbContext>();
        var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
        var usersRepository = scope.ServiceProvider.GetRequiredService<IUsersRepository>();
        var roleAssignmentService = scope.ServiceProvider.GetRequiredService<IUserRoleAssignmentService>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        
        var adminSection = configuration.GetRequiredSection("Admin");
        
        var adminLogin = adminSection.GetValue<string>("Login");
        if (string.IsNullOrEmpty(adminLogin))
        {
            throw new InvalidOperationException("Admin login is required to seed admin.");
        }
        var adminEmail = adminSection.GetValue<string>("Email");
        if (string.IsNullOrEmpty(adminEmail))
        {
            throw new InvalidOperationException("Admin email is required to seed admin.");
        }
        var adminPassword = adminSection.GetValue<string>("Password");
        if (string.IsNullOrEmpty(adminPassword))
        {
            throw new InvalidOperationException("Admin adminPassword is required to seed admin.");
        }
        
        var adminExists = await dbContext.Users.AnyAsync(u => u.Username == adminLogin);
        if (adminExists)
        {
            return;
        }

        var passwordHash = hasher.Hash(adminPassword);

        UserId? adminUserId = null;
        await using var transaction = await unitOfWork.BeginTransactionAsync(IsolationLevel.ReadCommitted, CancellationToken.None);
        try
        {
            var adminUser = (await UserEntity.CreateAsync(adminLogin, adminEmail, passwordHash, usersRepository)).Value;
            dbContext.Add(adminUser);
            await dbContext.SaveChangesAsync();

            adminUserId = adminUser.Id;
            await roleAssignmentService.AssignRoleToUserAsync(adminUserId, Role.Admin.Name);
            await dbContext.SaveChangesAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
        finally
        {
            await transaction.CommitAsync();
        }
        
        logger.LogWarning("Seed admin user with login: '{AdminLogin}'. UserId: '{UserId}'", adminLogin, adminUserId.Value);
    }
}