using GameReviews.Application.Common.Models.Dtos;

namespace GameReviews.Infrastructure.Services.IGDB;

public interface IIGDBService
{
    public Task<T[]> QueryAsync<T>(string endpoint, string query);
    public Task<IGDBResponseDto<T[]>> QueryWithResponseAsync<T>(string endpoint, string query);
    public Task<int> CountAsync(string endpoint, string query);
}