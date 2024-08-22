using GameReviews.Domain.Entities.RefreshToken;
using GameReviews.Domain.Entities.User;

namespace GameReviews.Application.Common.Interfaces.Repositories;
public interface IRefreshTokenRepository : IRepository<RefreshTokenEntity, RefreshTokenId>
{
    void RemoveTokens(UserId userId, bool onlyExpired = true);
    Task<RefreshTokenEntity?> GetTokenByUserIdAsync(string token, UserId userId);
}