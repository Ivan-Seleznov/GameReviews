using GameReviews.Application.Common.Models.Dtos.Game;
using GameReviews.Application.Common.Models.Dtos.User;

namespace GameReviews.Application.Common.Models.Dtos.Review;
public class ReviewDetailsDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Content { get; set; }

    public uint Rating { get; set; }
    public DateTime CreatedAt { get; set; }

    public UserInfoDto Author { get; set; }
    public GameInfoDto Game { get; set; }
}