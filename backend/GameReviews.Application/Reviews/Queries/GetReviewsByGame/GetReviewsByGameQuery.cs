using GameReviews.Application.Common;
using GameReviews.Application.Common.Interfaces.Query;
using GameReviews.Application.Common.Models.Dtos.Review;
using GameReviews.Application.Common.PagedList;
using GameReviews.Domain.Entities.GameAggregate.Entities;

namespace GameReviews.Application.Reviews.Queries.GetReviewsByGame;

public record GetReviewsByGameQuery(
    GameId GameId,     
    string SortOrder,
    int Page,
    int PageSize) : IQuery<PagedList<ReviewForGameDto>>, IPaginatedRequest
{
    public int PageSize { get; set; } = PageSize;
    public int Page { get; set; } = Page;
}