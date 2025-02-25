using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameReviews.Application.Common;
using GameReviews.Application.Common.Constants;
using GameReviews.Application.Common.Interfaces;
using GameReviews.Application.Common.Interfaces.Query;
using GameReviews.Application.Common.Models.Dtos.Review;
using GameReviews.Application.Common.Models.ReadEntities;
using GameReviews.Application.Common.PagedList;
using GameReviews.Domain.Results;
using Microsoft.EntityFrameworkCore;

namespace GameReviews.Application.Reviews.Queries.GetReviews;

internal class GetReviewsQueryHandler : IQueryHandler<GetReviewsQuery,PagedList<ReviewDetailsDto>>
{
    private readonly IReadApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetReviewsQueryHandler(IReadApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Result<PagedList<ReviewDetailsDto>>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<ReviewReadEntity> query = _context.Reviews
                .Include(r => r.Game)
                .Include(r => r.Author);
        
        query = ApplyFilters(query, request.Filter);

        return await PagedList<ReviewDetailsDto>.CreateWithQueryAsync(
            query.ProjectTo<ReviewDetailsDto>(_mapper.ConfigurationProvider),
            request.Page ?? 1,
            request.PageSize ?? PagingDefaults.FilterPageSize,
            cancellationToken);
    }

    private static IQueryable<ReviewReadEntity> ApplyFilters(IQueryable<ReviewReadEntity> query,
        ReviewsFilterParams filter)
    {
        if (filter.StartDate is not null)
        {
            query = query.Where(r => r.CreatedAt >= filter.StartDate);
        }

        if (filter.EndDate is not null)
        {
            query = query.Where(r => r.CreatedAt <= filter.EndDate);
        }

        if (filter.MinRating is not null)
        {
            query = query.Where(r => r.Rating >= filter.MinRating);
        }

        if (filter.MaxRating is not null)
        {
            query = query.Where(r => r.Rating <= filter.MaxRating);
        }
        
        if (!string.IsNullOrEmpty(filter.GameTitle))
        {
            query = query.Where(r => r.Game.Name.Contains(filter.GameTitle));
        }

        if (!string.IsNullOrEmpty(filter.AuthorUserName))
        {
            query = query.Where(r => r.Author.Username == filter.AuthorUserName);
        }
        
        return query;
    }
}