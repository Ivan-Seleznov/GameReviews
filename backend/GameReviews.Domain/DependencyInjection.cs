using GameReviews.Domain.Common.Abstractions.Services;
using GameReviews.Domain.Entities.UserGameRelationAggregate.Services;
using GameReviews.Domain.Entities.UserRoleAggregate.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GameReviews.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IGameUserRelationshipService, GameUserRelationshipService>();
        services.AddScoped<IUserRoleAssignmentService, UserRoleAssignmentService>();
        
        return services;
    }
}