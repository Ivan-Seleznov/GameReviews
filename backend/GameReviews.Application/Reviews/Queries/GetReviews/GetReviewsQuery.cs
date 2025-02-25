using GameReviews.Application.Common;
using GameReviews.Application.Common.Interfaces.Query;
using GameReviews.Application.Common.Models.Dtos.Review;
using GameReviews.Application.Common.PagedList;

namespace GameReviews.Application.Reviews.Queries.GetReviews;
public record ReviewsFilterParams(
    DateTime? StartDate,
    DateTime? EndDate,
    uint? MinRating,
    uint? MaxRating,
    string? AuthorUserName,
    string? GameTitle);

public record GetReviewsQuery(
    ReviewsFilterParams Filter,
    string SortColumn,
    string SortOrder,
    int? Page,
    int? PageSize) : IQuery<PagedList<ReviewDetailsDto>>;