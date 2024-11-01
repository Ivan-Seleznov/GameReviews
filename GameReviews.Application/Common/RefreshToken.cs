namespace GameReviews.Application.Common;

public record struct RefreshToken(string Token, DateTime ExpiresIn);