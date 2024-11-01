using GameReviews.Domain.Common.Abstractions.Entities;
using GameReviews.Domain.Entities.GameAggregate.Entities;
using GameReviews.Domain.Entities.UserAggregate.Entities;
using GameReviews.Domain.Results;

namespace GameReviews.Domain.Entities.ReviewAggregate.Entities;
public class ReviewEntity : BaseEntity<ReviewId>
{
    public string Title { get; private set; }
    public string? Content { get; private set; }

    public uint Rating { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public UserId AuthorId { get; private set; }
    public GameId GameId { get; private set; }

    private ReviewEntity() { }
    private ReviewEntity(
        string title, 
        string? content,
        uint rating,
        UserId authorId, 
        GameId gameId, 
        DateTime createdAt)
    {
        Title = title;
        Content = content;
        Rating = rating;
        AuthorId = authorId;
        GameId = gameId;
        CreatedAt = createdAt;
    }

    public static Result<ReviewEntity> Create(        
        string title, 
        string? content,
        uint rating,
        UserId authorId, 
        GameId gameId)
    {
        //Add authorId & gameId check?
        var createdAt = DateTime.UtcNow;
        var review = new ReviewEntity(title, content, rating, authorId, gameId, createdAt);

        return review;
    }
}
public record ReviewId(int Value) : EntityId<int>(Value);