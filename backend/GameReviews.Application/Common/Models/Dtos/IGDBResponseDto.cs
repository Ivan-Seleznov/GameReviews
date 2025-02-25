namespace GameReviews.Application.Common.Models.Dtos;

public record IGDBResponseDto<T>(
    T DeserializedContent,
    HttpResponseMessage ResponseMessage,
    string? StringContent);