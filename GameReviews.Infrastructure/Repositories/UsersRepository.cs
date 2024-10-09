using System.Runtime.CompilerServices;
using GameReviews.Application.Common.Interfaces.Repositories;
using GameReviews.Domain.Entities.Game;
using GameReviews.Domain.Entities.User;
using GameReviews.Domain.Entities.UserGame;
using GameReviews.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameReviews.Infrastructure.Repositories;

internal sealed class UsersRepository(ApplicationDbContext context)
    : Repository<UserEntity, UserId>(context), IUsersRepository
{
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
        return await context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<UserEntity?> GetByEmailAsync(string email)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<bool> UserHasGameAsync(UserId userId, GameId gameId)
    {
        return await context.UsersGames.AnyAsync(x => x.UsersId == userId && x.GamesId == gameId);
    }

    public async Task CreateOrAddGameToUser(UserId userId, GameEntity gameEntity)
    {
        if (!await context.Users.AnyAsync(u => u.Id == userId))
        {
            throw new Exception("User with this id does not exist");
        }
        await context.Games.AddAsync(gameEntity);
        await context.UsersGames.AddAsync(new GameEntityUserEntity { UsersId = userId, GamesId = gameEntity.Id });
    }
    public async Task AddGameToUser(UserId userId, GameId gameId)
    {
        await context.UsersGames.AddAsync(new GameEntityUserEntity { UsersId = userId, GamesId = gameId });
    }

    public async Task<UserEntity> GetUserOrThrow(UserId userId)
    {
        return await context.Users
                   .FirstOrDefaultAsync(u => u.Id == userId)
               ?? throw new Exception($"User with id {userId.Value} does not exist");
    }
}