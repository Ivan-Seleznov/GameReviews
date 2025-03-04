using GameReviews.Application.Common.Models.Dtos.Image;

namespace GameReviews.Application.Common.Models.Dtos.Platform;

public class PlatformDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ImageDto? Logo { get; set; }
}