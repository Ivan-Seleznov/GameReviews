using GameReviews.Domain.Common;
using GameReviews.Domain.Entities.User;

namespace GameReviews.Domain.Entities.RefreshToken;
public class RefreshTokenEntity : BaseEntity<RefreshTokenId>
{
    public string Token { get; set; }
    public DateTime ExpiresIn { get; set; }

    public UserId UserId { get; set; }
    public UserEntity User { get; set; }

    public bool IsActive => DateTime.UtcNow <= ExpiresIn;
}