using GameReviews.Application.Common;
using GameReviews.Application.Common.Interfaces;
using GameReviews.Application.Common.Interfaces.Authentication;
using GameReviews.Infrastructure.Authentication;
using GameReviews.Infrastructure.Data;
using GameReviews.Infrastructure.Data.Extensions;
using Igdb.Abstractions;
using IgdbApi;
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

        //repositories
        services.AddRepositories();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPermissionService, PermissionService>();

        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<IRefreshTokenProvider, RefreshTokenProvider>();

        var igdbSection = configuration.GetRequiredSection("Igdb");
        services.AddTransient<IIgdbClient, IgdbClient>(x 
            => new IgdbClient(igdbSection.GetRequiredSection("IgdbToken").Value!, igdbSection.GetRequiredSection("IgdbClient").Value!));
        
        return services;
    }
}