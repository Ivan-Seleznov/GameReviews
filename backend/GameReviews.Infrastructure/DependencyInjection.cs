using GameReviews.Application.Common;
using GameReviews.Application.Common.Interfaces;
using GameReviews.Application.Common.Interfaces.Authentication;
using GameReviews.Infrastructure.Authentication;
using GameReviews.Infrastructure.Data;
using GameReviews.Infrastructure.Data.Extensions;
using GameReviews.Infrastructure.Data.Interceptors;
using GameReviews.Infrastructure.Services;
using GameReviews.Infrastructure.Services.IGDB;
using IGDB;
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

        services.AddDbContext<ApplicationWriteDbContext>((sp,opt) =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("Default"));
            opt.AddInterceptors(sp.GetRequiredService<PublishDomainEventsInterceptor>());
        });
        services.AddDbContext<ApplicationReadDbContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("Default"));
            opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
        
        services.AddScoped<IReadApplicationDbContext, ApplicationReadDbContext>();
        services.AddScoped<PublishDomainEventsInterceptor>();
        
        services.AddRepositories();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPermissionService, PermissionService>();

        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<IRefreshTokenGenerator, RefreshTokenGenerator>();

        var igdbSection = configuration.GetRequiredSection("Igdb");
        
        services.AddTransient<IIGDBService, IGDBServiceAdapter>(x => new IGDBServiceAdapter(
            new IGDBClient(igdbSection.GetRequiredSection("ClientId").Value!,
                igdbSection.GetRequiredSection("ClientSecret").Value!)));

        services.AddTransient<IGameDetailsService, IGDBGameDetailsService>();
        
        return services;
    }
}