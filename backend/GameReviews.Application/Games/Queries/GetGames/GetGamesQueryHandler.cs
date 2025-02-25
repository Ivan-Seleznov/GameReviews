using GameReviews.Application.Common.Constants;
using GameReviews.Application.Common.Interfaces;
using GameReviews.Application.Common.Interfaces.Query;
using GameReviews.Application.Common.Models.Dtos.Game;
using GameReviews.Application.Common.PagedList;
using GameReviews.Domain.Results;

namespace GameReviews.Application.Games.Queries.GetGames;

internal class GetGamesQueryHandler : IQueryHandler<GetGamesQuery, PagedList<GameInfoDto>>
{
    private readonly IGameDetailsService _gameDetailsService;

    public GetGamesQueryHandler(IGameDetailsService gameDetailsService)
    {
        _gameDetailsService = gameDetailsService;
    }
    
    public async Task<Result<PagedList<GameInfoDto>>> Handle(GetGamesQuery request, CancellationToken cancellationToken)
    {
        var page = request.Page ?? 1;
        var pageSize = request.PageSize ?? PagingDefaults.FilterPageSize;
        
         return await _gameDetailsService.GetAllGamesAsync(
             request.Filter,
             request.SortColumn ?? "name",
             request.SortOrder,
             page,
             pageSize);
    }
}