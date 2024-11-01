using System.Security.Cryptography;
using GameReviews.Application.Common;

namespace GameReviews.Infrastructure.Authentication;
public class RefreshTokenGenerator : IRefreshTokenGenerator
{
    private const int BytesCount = 64;
    private const int ExpiresInDays = 30;

    public RefreshToken GenerateToken()
    {
        byte[] bytes = RandomNumberGenerator.GetBytes(BytesCount);
        
        var token = Convert.ToBase64String(bytes);
        var expiresIn = DateTime.UtcNow.AddDays(ExpiresInDays);

        return new RefreshToken(token,expiresIn);
    }
}
