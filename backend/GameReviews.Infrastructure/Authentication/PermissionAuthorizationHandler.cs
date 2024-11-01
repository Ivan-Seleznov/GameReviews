using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;
using GameReviews.Domain.Entities.UserAggregate;
using GameReviews.Domain.Entities.UserAggregate.Entities;

namespace GameReviews.Infrastructure.Authentication;
public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IServiceScopeFactory _scopeFactory;
    public PermissionAuthorizationHandler(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var rawUserId = context.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (!int.TryParse(rawUserId, out int userId))
        {
            return;
        }

        using IServiceScope scope = _scopeFactory.CreateScope();
        var permissionService = scope.ServiceProvider
            .GetRequiredService<IPermissionService>();

        var permissions = await permissionService
            .GetPermissionsAsync(new UserId(userId));

        if (permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }
    }
}