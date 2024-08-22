using GameReviews.Domain.Common;

namespace GameReviews.Domain.Entities.RefreshToken;
public record RefreshTokenId(int Value) : BaseEntityTypedId<int>(Value);