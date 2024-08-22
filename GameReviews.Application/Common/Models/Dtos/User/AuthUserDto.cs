namespace GameReviews.Application.Common.Models.Dtos.User;
public class AuthUserDto
{
    public UserDetailsDto User { get; set; }
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}