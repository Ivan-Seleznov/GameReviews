using GameReviews.Domain.Common.Abstractions.Entities;

namespace GameReviews.Domain.Entities.Review;

public record ReviewId(int Value) : BaseEntityTypedId<int>(Value);