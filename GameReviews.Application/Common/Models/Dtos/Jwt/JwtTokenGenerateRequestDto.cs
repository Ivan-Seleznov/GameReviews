using GameReviews.Domain.Entities.User;

namespace GameReviews.Application.Common.Models.Dtos.Jwt;

public class JwtTokenGenerateRequestDto
{
    public UserId Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
}

