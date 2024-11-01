namespace GameReviews.Application.Common;
public interface IRefreshTokenGenerator
{
    RefreshToken GenerateToken();
}