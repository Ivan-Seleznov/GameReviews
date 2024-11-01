using GameReviews.Application.Common.Interfaces.Query;
using GameReviews.Application.Common.Models.Dtos.Review;
using GameReviews.Domain.Entities.ReviewAggregate.Entities;

namespace GameReviews.Application.Reviews.Queries.GetReview;

public record GetReviewQuery(ReviewId ReviewId) : IQuery<ReviewDetailsDto>;