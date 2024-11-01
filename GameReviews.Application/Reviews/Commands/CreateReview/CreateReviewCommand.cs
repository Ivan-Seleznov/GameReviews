using GameReviews.Application.Common.Interfaces.Command;
using GameReviews.Application.Common.Models.Dtos.Review;
using GameReviews.Domain.Entities.GameAggregate.Entities;

namespace GameReviews.Application.Reviews.Commands.CreateReview;
public record CreateReviewCommand(
    GameId GameId,
    string Title,
    string? Content,
    uint Rating) : ICommand<ReviewDetailsDto>;