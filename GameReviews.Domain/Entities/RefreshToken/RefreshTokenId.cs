using GameReviews.Domain.Common.Abstractions.Entities;

namespace GameReviews.Domain.Entities.RefreshToken;
public record RefreshTokenId(int Value) : BaseEntityTypedId<int>(Value);