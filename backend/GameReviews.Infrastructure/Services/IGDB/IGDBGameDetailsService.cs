using GameReviews.Application.Common.Interfaces;
using GameReviews.Application.Common.Models.Dtos.Game;
using GameReviews.Application.Common.Models.Dtos.Genre;
using GameReviews.Application.Common.Models.Dtos.Image;
using GameReviews.Application.Common.Models.Dtos.Platform;
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
        var game = (await _igdbService.QueryAsync<Game>(IGDBClient.Endpoints.Games, $"fields {IGDBFieldConstants.GameDetails}; where id = {id};", cancellationToken))
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
                Url = game.Url,
            } : null,
            Screenshots = game.Screenshots?.Values.Select(s => new ImageDto
            {
                Height = s.Height,
                Width = s.Width,
                Url = s.Url,
            }).ToList(),
        };
    }
    
    public async Task<PagedList<GameInfoDto>> GetGamesAsync(GameFilter requestFilter, string requestSortColumn, string requestSortOrder, int page,
        int pageSize, CancellationToken cancellationToken = default)
    {
        var filters = new List<string>();

        if (!string.IsNullOrEmpty(requestFilter.Category) && Enum.TryParse<Category>(requestFilter.Category, true, out var categoryEnum))
        {
            filters.Add($"category = {((int)categoryEnum)}");
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

        if (requestFilter.Platforms != null && requestFilter.Platforms.Length > 0)
        { 
            var platformFilters = requestFilter.Platforms
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .Select(p => $"platforms.abbreviation = \"{p}\"").ToList();
            if (platformFilters.Count > 0)
            {
                filters.Add(string.Join(" | ", platformFilters));
            }
        }

        if (requestFilter.Genres != null && requestFilter.Genres.Length > 0)
        { 
            var genreFilters = requestFilter.Genres
                .Where(g => !string.IsNullOrWhiteSpace(g))
                .Select(g => $"genres.name = \"{g}\"").ToList();
            if (genreFilters.Count > 0)
            {
                filters.Add(string.Join(" | ", genreFilters));
            }
        }
        
        var whereClause = filters.Any() ? $"where {string.Join(" & ", filters)};" : "";

        var sortOrder = string.IsNullOrEmpty(requestSortOrder) ? "desc" : requestSortOrder;
        var sortClause = !string.IsNullOrEmpty(requestSortColumn) ? $"sort {requestSortColumn} {sortOrder};" : "";

        var query = $"fields {IGDBFieldConstants.GameDetails}; {whereClause} {sortClause} limit {pageSize}; offset {(page - 1) * pageSize};";

        var games = await _igdbService.QueryAsync<Game>(IGDBClient.Endpoints.Games, query, cancellationToken);
        
        if (games.Length == 0)
        {
            return PagedList<GameInfoDto>.CreateEmpty(page, pageSize);
        }
        
        var count = await _igdbService.CountAsync(IGDBClient.Endpoints.Games, query, cancellationToken);
        return games.Select((game) => new GameInfoDto
        {
            Id = game.Id ?? throw new Exception("Game id can not be null"),
            Name = game.Name,
            Description = game.Summary,
            Cover = game.Cover != null ? new ImageDto
            {
                Width = game.Cover.Value.Width,
                Height = game.Cover.Value.Height,
                Url = game.Cover.Value.Url,
            } : null,
        }).ToPagedList(page, pageSize, count);
    }
    
    public async Task<PagedList<GameInfoDto>> SearchGameAsync(string searchTerm, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var query =
            $"fields {IGDBFieldConstants.GameInfo};search \"${searchTerm}\"; limit {pageSize}; offset {(page - 1) * pageSize};";
        
        var games = await _igdbService.QueryAsync<Game>(IGDBClient.Endpoints.Games,query, cancellationToken);
        if (games.Length == 0)
        {
            return PagedList<GameInfoDto>.CreateEmpty(page, pageSize);
        }
        
        var count = await _igdbService.CountAsync(IGDBClient.Endpoints.Games, query, cancellationToken);
        return games.Select((game) => new GameInfoDto
        {
            Id = game.Id ?? throw new Exception("Game id can not be null"),
            Name = game.Name,
            Description = game.Summary,
            Cover = game.Cover != null ? new ImageDto
            {
                Width = game.Cover.Value.Width,
                Height = game.Cover.Value.Height,
                Url = game.Cover.Value.Url,
            } : null,
        }).ToPagedList(page, pageSize, count);
    }

    public async Task<PagedList<GenreDto>> GetGenresAsync(string requestSortColumn, string requestSortOrder, int page, int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query =
            $"fields {IGDBFieldConstants.Genre};sort {requestSortColumn} {requestSortOrder};limit {pageSize}; offset {(page - 1) * pageSize};";

        var genres = await _igdbService.QueryAsync<Genre>(IGDBClient.Endpoints.Genres, query, cancellationToken);
        if (genres.Length == 0)
        {
            return PagedList<GenreDto>.CreateEmpty(page, pageSize);
        }
        
        var count = await _igdbService.CountAsync(IGDBClient.Endpoints.Genres, query, cancellationToken);
        return genres.Select((genre) => new GenreDto
        {
            Id = genre.Id ?? throw new Exception("Genre id can not be null"),
            Name = genre.Name,
        }).ToPagedList(page, pageSize, count);
    }

    public async Task<PagedList<PlatformDto>> GetPlatformsAsync(string requestSortColumn, string requestSortOrder, int page, int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query =
            $"fields {IGDBFieldConstants.Platform};sort {requestSortColumn} {requestSortOrder};limit {pageSize}; offset {(page - 1) * pageSize};";

        var platforms = await _igdbService.QueryAsync<Platform>(IGDBClient.Endpoints.Platforms, query, cancellationToken);
        if (platforms.Length == 0)
        {
            return PagedList<PlatformDto>.CreateEmpty(page, pageSize);
        }
        
        var count = await _igdbService.CountAsync(IGDBClient.Endpoints.Platforms, query, cancellationToken);
        return platforms.Select((platform) => new PlatformDto
        {
            Id = platform.Id ?? throw new Exception("Platform id can not be null"),
            Name = platform.Abbreviation ?? platform.Name,
            Description = platform.Summary,
            Logo = platform.PlatformLogo != null ? new ImageDto
            {
                Height = platform.PlatformLogo.Value.Height,
                Width = platform.PlatformLogo.Value.Width,
                Url = platform.PlatformLogo.Value.Url,
            } : null
        }).ToPagedList(page, pageSize, count);
    }
}