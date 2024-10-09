using GameReviews.Domain.Common.Abstractions.Entities;
using GameReviews.Domain.Entities.Game;
using GameReviews.Domain.Entities.User;

namespace GameReviews.Domain.Entities.Review;
public class ReviewEntity : BaseEntity<ReviewId>
{
    public string Title { get; set; }
    public string? Content { get; set; }

    public uint Rating { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public UserId AuthorId { get; set; }
    public GameId GameId { get; set; }
    public UserEntity Author { get; set; }
    public GameEntity Game { get; set; }
}
