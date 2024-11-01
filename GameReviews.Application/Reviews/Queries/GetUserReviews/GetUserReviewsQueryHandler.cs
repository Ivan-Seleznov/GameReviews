using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameReviews.Application.Common;
using GameReviews.Application.Common.Interfaces;
using GameReviews.Application.Common.Interfaces.Query;
using GameReviews.Application.Common.Models.Dtos.Review;
using GameReviews.Domain.Results;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using GameReviews.Application.Common.Constants;
using GameReviews.Application.Common.Models.ReadEntities;

namespace GameReviews.Application.Reviews.Queries.GetUserReviews;
internal class GetUserReviewsQueryHandler : IQueryHandler<GetUserReviewsQuery, PagedList<ReviewDetailsDto>>
{
    private readonly IReadApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUserIdStorage _userIdStorage;
    public GetUserReviewsQueryHandler(IReadApplicationDbContext context, IUserIdStorage userIdStorage, IMapper mapper)
    {
        _context = context;
        _userIdStorage = userIdStorage;
        _mapper = mapper;
    }

    public async Task<Result<PagedList<ReviewDetailsDto>>> Handle(GetUserReviewsQuery request, CancellationToken cancellationToken)
    {
        var userId = _userIdStorage.UserId!;

        IQueryable<ReviewReadEntity> reviewsQuery = _context.Reviews.Where(r => r.AuthorId == userId)
            .Include(r => r.Game);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            reviewsQuery = reviewsQuery.Where(r => r.Game.Name.Contains(request.SearchTerm));
        }

        var keySelector = GetSortProperty(request.SortColumn);
        reviewsQuery = request.SortOrder?.ToLower() == "desc" ? reviewsQuery.OrderByDescending(keySelector) : reviewsQuery.OrderBy(keySelector);

        return await PagedList<ReviewDetailsDto>.CreateWithQueryAsync(
            reviewsQuery.ProjectTo<ReviewDetailsDto>(_mapper.ConfigurationProvider),
            request.Page ?? 1, 
            request.PageSize ?? PagingDefaults.FilterPageSize);
    }

    private static Expression<Func<ReviewReadEntity, object>> GetSortProperty(string? sortColumn)
    {
        return sortColumn?.ToLower() switch
        {
            "createdat" => review => review.CreatedAt,
            "gameName" => review => review.Game.Name,
            _ => review => review.Title
        };
    }
}
