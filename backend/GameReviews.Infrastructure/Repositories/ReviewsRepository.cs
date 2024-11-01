using GameReviews.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using GameReviews.Domain.Common.Abstractions.Repositories;
using GameReviews.Domain.Entities.GameAggregate.Entities;
using GameReviews.Domain.Entities.ReviewAggregate.Entities;
using GameReviews.Domain.Entities.UserAggregate;
using GameReviews.Domain.Entities.UserAggregate.Entities;

namespace GameReviews.Infrastructure.Repositories;
internal class ReviewsRepository(ApplicationWriteDbContext context) : Repository<ReviewEntity, ReviewId>(context), IReviewsRepository
{
    public async Task<bool> ExistsAsync(UserId authorId, GameId gameId)
    {
        return await context.Reviews.AnyAsync(r => r.GameId == gameId && r.AuthorId == authorId);
    }
}