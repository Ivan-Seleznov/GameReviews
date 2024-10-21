using GameReviews.Domain.Entities.Game;
using GameReviews.Domain.Entities.User;
using GameReviews.Domain.Results.Errors;

namespace GameReviews.Application.Common.Errors;
public static class GameErrors
{
    public static Error NotFound(GameId gameId) => new Error("Error.GameNotFound",
        $"Game with ID {gameId.Value} not found", ErrorType.NotFound);
}