using GameReviews.Application.Common.Interfaces.Repositories;
using GameReviews.Domain.Entities.Game;
using GameReviews.Domain.Entities.Review;
using GameReviews.Domain.Entities.User;
using GameReviews.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameReviews.Infrastructure.Repositories;
internal class ReviewsRepository(ApplicationDbContext context) : Repository<ReviewEntity, ReviewId>(context), IReviewsRepository
{
    public async Task<bool> ExistsAsync(UserId authorId, GameId gameId)
    {
        return await context.Reviews.AnyAsync(r => r.GameId == gameId && r.AuthorId == authorId);
    }

    public async Task<ReviewEntity?> GetByIdWithRelatedEntitiesAsync(ReviewId reviewId)
    {
        return await context.Reviews
            .Include(r => r.Game)
            .Include(r => r.Author)
            .FirstOrDefaultAsync(r => r.Id == reviewId);
    }
}