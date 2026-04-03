namespace GameReviews.Application.Common.Models.Dtos.Review;

public class ReviewForGameDto
{
    public required string Title { get; set; }
    public string? Content { get; set; }

    public uint Rating { get; set; }
    public DateTime CreatedAt { get; set; }

    public required string AuthorName { get; set; }
    public int AuthorId { get; set; }
}