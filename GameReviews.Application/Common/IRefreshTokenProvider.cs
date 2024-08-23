using GameReviews.Domain.Entities.RefreshToken;
using GameReviews.Domain.Entities.User;

namespace GameReviews.Application.Common;
public interface IRefreshTokenProvider
{
    RefreshTokenEntity GenerateToken(UserId userId);
}