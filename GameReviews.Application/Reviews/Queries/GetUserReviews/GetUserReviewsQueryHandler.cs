using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameReviews.Application.Common;
using GameReviews.Application.Common.Interfaces;
using GameReviews.Application.Common.Interfaces.Query;
using GameReviews.Application.Common.Models.Dtos.Review;
using GameReviews.Domain.Common.Result;
using GameReviews.Domain.Entities.Review;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GameReviews.Application.Reviews.Queries.GetUserReviews;
internal class GetUserReviewsQueryHandler : IQueryHandler<GetUserReviewsQuery, PagedList<ReviewDetailsDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUserIdStorage _userIdStorage;
    public GetUserReviewsQueryHandler(IApplicationDbContext context, IUserIdStorage userIdStorage, IMapper mapper)
    {
        _context = context;
        _userIdStorage = userIdStorage;
        _mapper = mapper;
    }

    public async Task<Result<PagedList<ReviewDetailsDto>>> Handle(GetUserReviewsQuery request, CancellationToken cancellationToken)
    {
        var userId = _userIdStorage.UserId!;

        IQueryable<ReviewEntity> reviewsQuery = _context.Reviews.Where(r => r.AuthorId == userId)
            .AsNoTracking()
            .Include(r => r.Game);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            reviewsQuery = reviewsQuery.Where(r => r.Game.Name.Contains(request.SearchTerm));
        }

        var keySelector = GetSortProperty(request.SortColumn);
        reviewsQuery = request.SortOrder?.ToLower() == "desc" ? reviewsQuery.OrderByDescending(keySelector) : reviewsQuery.OrderBy(keySelector);

        var projectedQuery = reviewsQuery.ProjectTo<ReviewDetailsDto>(_mapper.ConfigurationProvider);
        return await PagedList<ReviewDetailsDto>.CreateAsync(projectedQuery, request.Page, request.PageSize);
    }

    private static Expression<Func<ReviewEntity, object>> GetSortProperty(string? sortColumn)
    {
        return sortColumn?.ToLower() switch
        {
            "createdat" => review => review.CreatedAt,
            "gameName" => review => review.Game.Name,
            _ => review => review.Title
        };
    }
}
