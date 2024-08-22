using GameReviews.Domain.Common;

namespace GameReviews.Domain.Entities.User;

public record UserId(int Value) : BaseEntityTypedId<int>(Value);