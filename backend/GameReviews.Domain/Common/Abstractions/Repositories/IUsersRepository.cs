using GameReviews.Domain.Entities.GameAggregate.Entities;
using GameReviews.Domain.Entities.UserAggregate.Entities;
using GameReviews.Domain.Entities.UserGameRelationAggregate.Entities;

namespace GameReviews.Domain.Common.Abstractions.Repositories;
public interface IUsersRepository : IRepository<UserEntity, UserId>
{
    Task AddUserGameRelationAsync(GameUserRelationship gameUserRelation);
    Task<bool> IsEmailExistsAsync(string email);
    Task<bool> IsUsernameExistsAsync(string username);
    Task<UserEntity?> GetByUsernameAsync(string username);
    Task<UserEntity?> GetByEmailAsync(string email);
    Task<UserEntity?> GetWithRefreshTokens(UserId id);
    Task<bool> UserHasGameAsync(UserId userId, GameId requestGameId);
}