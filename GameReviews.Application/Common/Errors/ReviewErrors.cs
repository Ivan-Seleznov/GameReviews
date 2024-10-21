using GameReviews.Domain.Entities.Game;
using GameReviews.Domain.Entities.Review;
using GameReviews.Domain.Entities.User;
using GameReviews.Domain.Results.Errors;

namespace GameReviews.Application.Common.Errors;
public static class ReviewErrors
{
    public static Error NotFound(ReviewId reviewId) => new Error("Error.Review.NotFound",
        $"Review with ID {reviewId.Value} not found", ErrorType.NotFound);
    public static Error AlreadyExists(UserId userId, GameId gameId) => new Error("Error.Review.AlreadyExist",
        $"Review with userId {userId.Value} and gameId {gameId.Value} already exists", ErrorType.Failure);
}