using GameReviews.Domain.Common.Result.Errors;
using GameReviews.Domain.Entities.Game;
using GameReviews.Domain.Entities.User;

namespace GameReviews.Application.Common.Errors;
public static class UserErrors
{
    public static Error NotFound(UserId userId) => new Error("Error.UserNotFound",
        $"User with ID {userId.Value} not found", ErrorType.NotFound);
    public static Error AlreadyHasGame(UserId userId, GameId gameId) => new Error("Error.UserAlreadyHasGame",
        $"User with ID {userId.Value} already has game with ID {gameId.Value}",ErrorType.Failure);
}