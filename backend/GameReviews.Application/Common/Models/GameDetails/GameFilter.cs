namespace GameReviews.Application.Common.Models.GameDetails;

public record GameFilter(
    string? Type,
    string? Status,
    DateTime? EndYear,
    DateTime? StartYear,
    long[]? PlatformIds);