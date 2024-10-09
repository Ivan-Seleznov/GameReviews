using GameReviews.Domain.Entities.Roles;
using GameReviews.Domain.Entities.RefreshToken;
using GameReviews.Domain.Common.Abstractions.Entities;
using GameReviews.Domain.Entities.Game;


namespace GameReviews.Domain.Entities.User;

public class UserEntity : BaseDomainEntity<UserId>
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public ICollection<RefreshTokenEntity> RefreshTokens { get; set; }

    public ICollection<GameEntity> Games { get; set; } = new List<GameEntity>();

    public ICollection<Role> Roles { get; set; } = new List<Role>();
}