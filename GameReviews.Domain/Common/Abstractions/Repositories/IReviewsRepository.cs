using GameReviews.Domain.Entities.GameAggregate.Entities;
using GameReviews.Domain.Entities.ReviewAggregate.Entities;
using GameReviews.Domain.Entities.UserAggregate.Entities;

namespace GameReviews.Domain.Common.Abstractions.Repositories;
public interface IReviewsRepository : IRepository<ReviewEntity,ReviewId>
{
    Task<bool> ExistsAsync(UserId authorId, GameId gameId);
}