using GameReviews.Application.Common.Models.Dtos.Image;

namespace GameReviews.Application.Common.Models.Dtos.Game;
public class GameInfoDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ImageDto? Cover { get; set; }
}