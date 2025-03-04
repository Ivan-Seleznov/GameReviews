using GameReviews.Application.Common.Models.Dtos.Game;
using GameReviews.Application.Common.Models.Dtos.Genre;
using GameReviews.Application.Common.Models.Dtos.Platform;
using GameReviews.Application.Common.Models.GameDetails;
using GameReviews.Application.Common.PagedList;
using GameReviews.Domain.Results;

namespace GameReviews.Application.Common.Interfaces;

public interface IGameDetailsService
{
    public Task<GameDetailsDto?> GetGameByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<PagedList<GameInfoDto>> GetGamesAsync(GameFilter requestFilter, string requestSortColumn, string requestSortOrder, int page, int pageSize, CancellationToken cancellationToken = default);
    Task<PagedList<GameInfoDto>> SearchGameAsync(string searchTerm, int page, int pageSize, CancellationToken cancellationToken = default);
    Task<PagedList<GenreDto>> GetGenresAsync(string requestSortColumn, string requestSortOrder, int page, int pageSize, CancellationToken cancellationToken = default);
    Task<PagedList<PlatformDto>> GetPlatformsAsync(string requestSortColumn, string requestSortOrder, int requestPage, int requestPageSize, CancellationToken cancellationToken = default);
}