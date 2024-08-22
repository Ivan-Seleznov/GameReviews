using GameReviews.Application.Common.Interfaces.Repositories;
using GameReviews.Domain.Entities.RefreshToken;
using GameReviews.Domain.Entities.User;
using GameReviews.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameReviews.Infrastructure.Repositories;
internal class RefreshTokenRepository(ApplicationDbContext context) : Repository<RefreshTokenEntity, RefreshTokenId>(context), IRefreshTokenRepository
{
    public void RemoveTokens(UserId userId, bool onlyExpired)
    {
        var tokens = context.Set<RefreshTokenEntity>().Where(r => r.UserId == userId).AsEnumerable();
        context.RemoveRange(tokens.Where(x => !onlyExpired || !x.IsActive));
    }

    public async Task<RefreshTokenEntity?> GetTokenByUserIdAsync(string token, UserId userId)
    {
        return await context.Set<RefreshTokenEntity>()
            .FirstOrDefaultAsync(x => x.Token == token && x.UserId == userId);
    }
}