namespace GameReviews.Application.Common.Models.GameDetails;

public record GameFilter(
    string? Category,
    DateTime? EndYear,
    DateTime? StartYear,
    string[]? Platforms,
    string[]? Genres);