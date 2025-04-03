using GameReviews.Domain.Common.Abstractions.Repositories;
using GameReviews.Domain.Entities.GameAggregate.Entities;
using GameReviews.Domain.Entities.UserAggregate.Entities;
using GameReviews.Domain.Entities.UserGameRelationAggregate.Entities;
using GameReviews.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameReviews.Infrastructure.Repositories;

internal sealed class UsersRepository(ApplicationWriteDbContext context)
    : Repository<UserEntity, UserId>(context), IUsersRepository
{
    public async Task AddUserGameRelationAsync(GameUserRelationship gameUserRelation)
    {
        await context.UsersGames.AddAsync(gameUserRelation);
    }

    public async Task<UserEntity?> GetWithRefreshTokens(UserId id)
    {
        return await context.Users
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<bool> UserHasGameAsync(UserId userId, GameId gameId)
    {
        return await context.UsersGames.AnyAsync(x => x.UsersId == userId && x.GamesId == gameId);
    }
    
    public async Task<bool> IsEmailExistsAsync(string email)
    {
        return await context.Users.AnyAsync(u => u.Email == email);
    }
    public async Task<bool> IsUsernameExistsAsync(string userName)
    {
        return await context.Users.AnyAsync(u => u.Username == userName);
    }

    public async Task<UserEntity?> GetByUsernameAsync(string username)
    {
        return await context.Users
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.Username == username);
    }
    public async Task<UserEntity?> GetByEmailAsync(string email)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}