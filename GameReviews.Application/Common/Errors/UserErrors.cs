using GameReviews.Domain.Common.Result.Errors;
using GameReviews.Domain.Entities.User;

namespace GameReviews.Application.Common.Errors;
public static class UserErrors
{
    public static Error NotFound(UserId userId) => new Error("Error.UserNotFound",
        $"User with ID {userId.Value} not found", ErrorType.NotFound);
}