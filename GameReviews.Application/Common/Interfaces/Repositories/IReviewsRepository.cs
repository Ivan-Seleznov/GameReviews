using GameReviews.Domain.Entities.Game;
using GameReviews.Domain.Entities.Review;
using GameReviews.Domain.Entities.User;

namespace GameReviews.Application.Common.Interfaces.Repositories;
public interface IReviewsRepository : IRepository<ReviewEntity,ReviewId>
{
    Task<bool> ExistsAsync(UserId authorId, GameId gameId);
    Task<ReviewEntity?> GetByIdWithRelatedEntitiesAsync(ReviewId reviewId);
}