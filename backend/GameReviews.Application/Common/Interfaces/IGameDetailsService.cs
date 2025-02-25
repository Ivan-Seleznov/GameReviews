using GameReviews.Application.Common.Models.Dtos.Game;
using GameReviews.Application.Common.Models.GameDetails;
using GameReviews.Application.Common.PagedList;

namespace GameReviews.Application.Common.Interfaces;

public interface IGameDetailsService
{
    public Task<GameDetailsDto?> GetGameByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<PagedList<GameInfoDto>> GetAllGamesAsync(GameFilter requestFilter, string requestSortColumn, string? requestSortOrder, int page, int pageSize);
    Task<PagedList<GameInfoDto>> SearchGameAsync(string requestSearchTerm, int page, int pageSize);
}