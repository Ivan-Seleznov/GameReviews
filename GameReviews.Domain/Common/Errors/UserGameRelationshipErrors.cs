using GameReviews.Domain.Entities.GameAggregate.Entities;
using GameReviews.Domain.Entities.UserAggregate.Entities;
using GameReviews.Domain.Results.Errors;

namespace GameReviews.Domain.Common.Errors;

internal static class UserGameRelationshipErrors
{
    public static Error UserNotExist(UserId userId) => 
        new ("DomainError.UserNotExist", $"User {userId} does not exist", ErrorType.Failure);
    public static Error GameNotExist(GameId gameId) => 
        new ("DomainError.GameNotExist", $"Game {gameId} does not exist", ErrorType.Failure);
}