using GameReviews.Domain.Entities.UserAggregate.Entities;

namespace GameReviews.Application.Common.Models.ReadEntities;

public class UserReadEntity
{
    public UserId Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public ICollection<RefreshTokenReadEntity> RefreshTokens { get; set; } = new List<RefreshTokenReadEntity>();
    public ICollection<GameReadEntity> Games { get; set; } = new List<GameReadEntity>();
    public ICollection<RoleReadEntity> Roles { get; set; } = new List<RoleReadEntity>();
}