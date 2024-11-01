using GameReviews.Application.Common.Models.Dtos.Image;

namespace GameReviews.Application.Common.Models.Dtos.Game;

public class GameDetailsDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? Category { get; set; }
    public string? GameStatus { get; set; }
    public DateTimeOffset? ReleaseDate { get; set; }
    public ImageDto? Cover { get; set; }
    public List<ImageDto>? Screenshots { get; set; }
}