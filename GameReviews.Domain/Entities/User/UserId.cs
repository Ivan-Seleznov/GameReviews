using GameReviews.Domain.Common.Abstractions.Entities;

namespace GameReviews.Domain.Entities.User;
public record UserId(int Value) : BaseEntityTypedId<int>(Value);