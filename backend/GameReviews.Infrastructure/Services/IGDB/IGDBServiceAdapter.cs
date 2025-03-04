using GameReviews.Application.Common.Models.Dtos;
using IGDB;

namespace GameReviews.Infrastructure.Services.IGDB;

public class IGDBServiceAdapter(IGDBClient igdbClient) : IIGDBService
{
    private IGDBClient _igdbClient = igdbClient;

    public Task<T[]> QueryAsync<T>(string endpoint, string query, CancellationToken cancellationToken = default)
    {
        return _igdbClient.QueryAsync<T>(endpoint, query);
    }

    public async Task<IGDBResponseDto<T[]>> QueryWithResponseAsync<T>(string endpoint, string query, CancellationToken cancellationToken = default)
    {
        var response = await _igdbClient.QueryWithResponseAsync<T>(endpoint, query);
        return new IGDBResponseDto<T[]>(response.GetContent(), response.ResponseMessage, response.StringContent);
    }

    public async Task<int> CountAsync(string endpoint, string query, CancellationToken cancellationToken = default)
    {
        var count = await _igdbClient.CountAsync(endpoint, query);
        return count.Count;
    }
};
