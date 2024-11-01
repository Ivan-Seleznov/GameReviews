using GameReviews.Application.Common;
using GameReviews.Application.Common.Interfaces.Query;
using GameReviews.Application.Common.Models.Dtos.Review;

namespace GameReviews.Application.Reviews.Queries.GetUserReviews;
public record GetUserReviewsQuery(
    string? SearchTerm, 
    string? SortColumn, 
    string? SortOrder,
    int? Page, 
    int? PageSize) : IQuery<PagedList<ReviewDetailsDto>>;