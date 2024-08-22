using System.Security.Cryptography;
using GameReviews.Application.Common;
using GameReviews.Domain.Entities.RefreshToken;
using GameReviews.Domain.Entities.User;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GameReviews.Infrastructure.Authentication;
public class RefreshTokenProvider : IRefreshTokenProvider
{
    private const int BytesCount = 64;
    private const int ExpiresInDays = 30;

    public RefreshTokenEntity GenerateToken(UserId userId)
    {
        byte[] bytes = RandomNumberGenerator.GetBytes(BytesCount);
        var token = Convert.ToBase64String(bytes);

        return new RefreshTokenEntity
        {
            ExpiresIn = DateTime.UtcNow.AddDays(ExpiresInDays),
            Token = token,
            UserId = userId
        };
    }
}
