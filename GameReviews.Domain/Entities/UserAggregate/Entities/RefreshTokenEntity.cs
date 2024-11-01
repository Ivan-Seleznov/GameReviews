using GameReviews.Domain.Common.Abstractions.Entities;

namespace GameReviews.Domain.Entities.UserAggregate.Entities;
public class RefreshTokenEntity : BaseEntity<RefreshTokenId>
{
    public string Token { get; private set; }
    public DateTime ExpiresIn { get; private set; }
    public UserId UserId { get; private set; }
    public bool IsActive => DateTime.UtcNow <= ExpiresIn;
    
    private RefreshTokenEntity() { }
    internal RefreshTokenEntity(string token, DateTime expiresIn)
    {
        Token = token;
        ExpiresIn = expiresIn;
    }
}

public record RefreshTokenId(int Value) : EntityId<int>(Value);