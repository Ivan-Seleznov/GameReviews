using GameReviews.Domain.Entities.UserAggregate.Entities;

namespace GameReviews.Application.Common.Models.ReadEntities;
public class RefreshTokenReadEntity
{
    public RefreshTokenId Id { get; set; }
    public string Token { get; set; }
    public DateTime ExpiresIn { get; set; }

    public UserId UserId { get; set; }
    public UserReadEntity User { get; set; }

    public bool IsActive => DateTime.UtcNow <= ExpiresIn;
}