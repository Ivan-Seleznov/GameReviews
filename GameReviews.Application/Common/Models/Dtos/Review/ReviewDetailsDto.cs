using GameReviews.Application.Common.Models.Dtos.Game;
using GameReviews.Application.Common.Models.Dtos.User;

namespace GameReviews.Application.Common.Models.Dtos.Review;
public class ReviewDetailsDto
{
    public string Title { get; set; }
    public string? Content { get; set; }

    public uint Rating { get; set; }
    public DateTime CreatedAt { get; set; }

    public UserDetailsDto Author { get; set; }
    public GameDetailsDto Game { get; set; }
}