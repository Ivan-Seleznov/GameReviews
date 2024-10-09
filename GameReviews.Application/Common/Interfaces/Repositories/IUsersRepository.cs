using GameReviews.Domain.Entities.Game;
using GameReviews.Domain.Entities.User;

namespace GameReviews.Application.Common.Interfaces.Repositories;
public interface IUsersRepository : IRepository<UserEntity, UserId>
{
    Task<bool> IsEmailExistsAsync(string email);
    Task<bool> IsUsernameExistsAsync(string username);

    Task<UserEntity?> GetByUsernameAsync(string username);
    Task<UserEntity?> GetByEmailAsync(string email);
    Task CreateOrAddGameToUser(UserId userId, GameEntity gameEntity);
    Task AddGameToUser(UserId userId, GameId gameId);

    Task<bool> UserHasGameAsync(UserId userId, GameId gameId);
}