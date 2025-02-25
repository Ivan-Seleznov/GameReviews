using GameReviews.Application.Common;
using GameReviews.Application.Common.Constants;
using GameReviews.Application.Common.Interfaces;
using GameReviews.Application.Common.Interfaces.Query;
using GameReviews.Application.Common.Models.Dtos.Game;
using GameReviews.Application.Common.PagedList;
using GameReviews.Domain.Results;

namespace GameReviews.Application.Games.Queries.SearchGames;
internal class SearchGamesQueryHandler : IQueryHandler<SearchGamesQuery, PagedList<GameInfoDto>>
{
    private readonly IGameDetailsService _gameDetailsService;
    public SearchGamesQueryHandler(IGameDetailsService gameDetailsService)
    {
        _gameDetailsService = gameDetailsService;
    }
    public async Task<Result<PagedList<GameInfoDto>>> Handle(SearchGamesQuery request, CancellationToken cancellationToken)
    {
        var page = request.Page ?? 1;
        var pageSize = request.PageSize ?? PagingDefaults.SearchPageSize;

        if (string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            return PagedList<GameInfoDto>.Create([], page, pageSize,0);
        }
        
        return await _gameDetailsService.SearchGameAsync(request.SearchTerm, page, pageSize);
    }
}