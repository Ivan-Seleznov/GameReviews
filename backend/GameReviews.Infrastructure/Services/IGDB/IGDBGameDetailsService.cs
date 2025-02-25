using GameReviews.Application.Common.Interfaces;
using GameReviews.Application.Common.Models.Dtos.Game;
using GameReviews.Application.Common.Models.Dtos.Image;
using GameReviews.Application.Common.Models.GameDetails;
using GameReviews.Application.Common.PagedList;
using GameReviews.Infrastructure.Constants.IGDB;
using IGDB;
using IGDB.Models;

namespace GameReviews.Infrastructure.Services.IGDB;

public class IGDBGameDetailsService : IGameDetailsService
{
    private IIGDBService _igdbService;

    public IGDBGameDetailsService(IIGDBService igdbService)
    {
        _igdbService = igdbService;
    }

    public async Task<GameDetailsDto?> GetGameByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var game = (await _igdbService.QueryAsync<Game>(IGDBClient.Endpoints.Games, $"fields {IGDBFieldConstants.GameDetails}; where id = {id};"))
            .FirstOrDefault();
        
        return game is null ? null : new GameDetailsDto
        {
            Id = game.Id ?? throw new Exception("Game id can not be null"),
            Name = game.Name,
            Description = game.Summary,
            ReleaseDate = game.FirstReleaseDate,
            Category = game.Category?.ToString(),
            GameStatus = game.Status?.ToString(),
            Cover = game.Cover?.Value != null ? new ImageDto
            {
                Height = game.Cover.Value.Height,
                Width = game.Cover.Value.Width,
                Url = game.Url, //game.Cover.Value.GetUrlWithPixelCount()
            } : null,
            Screenshots = game.Screenshots?.Values.Select(s => new ImageDto
            {
                Height = s.Height,
                Width = s.Width,
                Url = s.Url, // s.GetUrlWithPixelCount()
            }).ToList(),
        };
    }
    
    public async Task<PagedList<GameInfoDto>> GetAllGamesAsync(GameFilter requestFilter, string requestSortColumn, string? requestSortOrder, int page,
        int pageSize)
    {
        var filters = new List<string>();

        if (!string.IsNullOrEmpty(requestFilter.Type))
        {
            filters.Add($"game_type.type = \"{requestFilter.Type}\"");
        }

        if (!string.IsNullOrEmpty(requestFilter.Status))
        {
            filters.Add($"game_status.status = \"{requestFilter.Status}\"");
        }

        if (requestFilter.StartYear.HasValue)
        {
            filters.Add(
                $"first_release_date >= {new DateTimeOffset(requestFilter.StartYear.Value).ToUnixTimeSeconds()}");
        }

        if (requestFilter.EndYear.HasValue)
        {
            filters.Add($"first_release_date <= {new DateTimeOffset(requestFilter.EndYear.Value).ToUnixTimeSeconds()}");
        }

        if (requestFilter.PlatformIds != null && requestFilter.PlatformIds.Any())
        {
            filters.Add($"platforms = ({string.Join(",", requestFilter.PlatformIds)})");
        }

        var whereClause = filters.Any() ? $"where {string.Join(" & ", filters)};" : "";

        var sortOrder = string.IsNullOrEmpty(requestSortOrder) ? "desc" : requestSortOrder;
        var sortClause = !string.IsNullOrEmpty(requestSortColumn) ? $"sort {requestSortColumn} {sortOrder};" : "";

        var query = $"fields {IGDBFieldConstants.GameDetails}; {whereClause} {sortClause} limit {pageSize}; offset {(page - 1) * pageSize};";

        var games = await _igdbService.QueryAsync<Game>(IGDBClient.Endpoints.Games, query);
        
        if (games.Length == 0)
        {
            return PagedList<GameInfoDto>.CreateEmpty(page, pageSize);
        }
        
        var count = await _igdbService.CountAsync(IGDBClient.Endpoints.Games, query);
        return games.Select((game) => new GameInfoDto
        {
            Id = game.Id ?? throw new Exception("Game id can not be null"),
            Name = game.Name,
            Description = game.Summary,
            Cover = game.Cover != null ? new ImageDto
            {
                Width = game.Cover.Value.Width,
                Height = game.Cover.Value.Height,
                Url = game.Cover.Value.Url, //game.Cover.Value.GetUrlWithPixelCount(720)
            } : null,
        }).ToPagedList(page, pageSize, count);
    }
    
    public async Task<PagedList<GameInfoDto>> SearchGameAsync(string requestSearchTerm, int page, int pageSize)
    {
        var query =
            $"fields {IGDBFieldConstants.GameInfo};search \"${requestSearchTerm}\"; limit {pageSize}; offset {(page - 1) * pageSize};";
        
        var games = await _igdbService.QueryAsync<Game>(IGDBClient.Endpoints.Games,query);
        if (games.Length == 0)
        {
            return PagedList<GameInfoDto>.CreateEmpty(page, pageSize);
        }
        
        var count = await _igdbService.CountAsync(IGDBClient.Endpoints.Games, query);
        return games.Select((game) => new GameInfoDto
        {
            Id = game.Id ?? throw new Exception("Game id can not be null"),
            Name = game.Name,
            Description = game.Summary,
            Cover = game.Cover != null ? new ImageDto
            {
                Width = game.Cover.Value.Width,
                Height = game.Cover.Value.Height,
                Url = game.Cover.Value.Url, //game.Cover.Value.GetUrlWithPixelCount(720)
            } : null,
        }).ToPagedList(page, pageSize, count);
    }
}