using GameReviews.Application.Common.Models.Dtos.Jwt;
using GameReviews.Domain.Entities.UserAggregate;
using GameReviews.Domain.Entities.UserAggregate.Entities;

namespace GameReviews.Application.Common.Interfaces.Authentication;
public interface IJwtProvider
{
    string GenerateToken(JwtTokenGenerateRequestDto jwtTokenGenerateRequest);
    UserId GetUserIdFromToken(string token);
}