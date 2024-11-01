using GameReviews.Domain.Common.Abstractions.Entities;
using GameReviews.Domain.Entities.GameAggregate.Entities;
using GameReviews.Domain.Entities.ReviewAggregate.Entities;
using GameReviews.Domain.Entities.UserAggregate.Entities;

namespace GameReviews.Application.Common.Models.ReadEntities;
public class ReviewReadEntity
{
    public ReviewId Id { get; set; }
    public string Title { get; set; }
    public string? Content { get; set; }

    public uint Rating { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public UserId AuthorId { get; set; }
    public GameId GameId { get; set; }
    public UserReadEntity Author { get; set; }
    public GameReadEntity Game { get; set; }
}