using GameReviews.Application.Common;
using GameReviews.Application.Common.Interfaces;
using GameReviews.Application.Common.Interfaces.Authentication;
using GameReviews.Application.Common.Interfaces.Repositories;
using GameReviews.Infrastructure.Authentication;
using GameReviews.Infrastructure.Data;
using GameReviews.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameReviews.Infrastructure;

public static class DependencyInjection
{
    
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(IGameReviewsInfrastructureMarker).Assembly;

        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            
            opt.UseNpgsql(configuration.GetConnectionString("Default"));
        });

        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IRolesRepository, RolesRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPermissionService, PermissionService>();

        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<IRefreshTokenProvider, RefreshTokenProvider>();

        return services;
    }
}

