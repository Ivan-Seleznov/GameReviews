using GameReviews.Application.Common.Interfaces;
using System.Security.Claims;
using GameReviews.Domain.Entities.UserAggregate;
using GameReviews.Domain.Entities.UserAggregate.Entities;

namespace GameReviews.Web.Middleware;
public class SaveUserIdMiddleware
{
    private readonly RequestDelegate _next;
    public SaveUserIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IUserIdStorage userIdStorage)
    {
        var claimsUserId = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        if (claimsUserId is not null && int.TryParse(claimsUserId, out var userId))
        {
            userIdStorage.UserId = new UserId(userId);
        }

        await _next(context);
    }
}