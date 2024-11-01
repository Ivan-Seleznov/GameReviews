using GameReviews.Application.Common.Models.Dtos.Game;

namespace GameReviews.Application.Common.Models.Dtos.User;
public class UserGamesDetailsDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }

    public ICollection<GameDetailsDto> Games { get; set; }
}
