using GameReviews.Application.Common.Constants;
using GameReviews.Application.Common.Interfaces;
using GameReviews.Application.Common.Interfaces.Query;
using GameReviews.Application.Common.Models.Dtos.Review;
using GameReviews.Application.Common.Models.ReadEntities;
using GameReviews.Application.Common.PagedList;
using GameReviews.Domain.Results;

namespace GameReviews.Application.Reviews.Queries.GetReviewsByGame;

public class GetReviewsByGameQueryHandler : IQueryHandler<GetReviewsByGameQuery, PagedList<ReviewForGameDto>>
{
    private readonly IReadApplicationDbContext _context;

    public GetReviewsByGameQueryHandler(IReadApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PagedList<ReviewForGameDto>>> Handle(GetReviewsByGameQuery request, CancellationToken cancellationToken)
    {
        IQueryable<ReviewReadEntity> query = _context.Reviews.Where(r => r.GameId == request.GameId);

        query = request.SortOrder == "asc" ? query.OrderBy(r => r.CreatedAt) : query.OrderByDescending(r => r.CreatedAt);
        
        return await PagedList<ReviewForGameDto>.CreateWithQueryAsync(
            query.Select(r => new ReviewForGameDto
            {
                AuthorId = r.AuthorId.Value,
                AuthorName = r.Author.Username,
                CreatedAt = r.CreatedAt,
                Content = r.Content,
                Title = r.Title,
                Rating = r.Rating,
            }),
            request.Page,
            request.PageSize,
            cancellationToken);
    }
}