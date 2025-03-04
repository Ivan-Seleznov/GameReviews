using GameReviews.Application.Common.Constants;
using GameReviews.Application.Common.Interfaces;
using GameReviews.Application.Common.Interfaces.Query;
using GameReviews.Application.Common.Models.Dtos.Genre;
using GameReviews.Application.Common.PagedList;
using GameReviews.Domain.Results;

namespace GameReviews.Application.Games.Queries.GetGameGenres;

public class GetGameGenresQueryHandler : IQueryHandler<GetGameGenresQuery, PagedList<GenreDto>>
{
    private readonly IGameDetailsService _gameDetailsService;
    public GetGameGenresQueryHandler(IGameDetailsService gameDetailsService)
    {
        _gameDetailsService = gameDetailsService;
    }
    
    public async Task<Result<PagedList<GenreDto>>> Handle(GetGameGenresQuery request, CancellationToken cancellationToken)
    {
        return await _gameDetailsService.GetGenresAsync(request.SortColumn ?? "name", request.SortOrder ?? "desc",request.Page ?? 1, request.PageSize ?? PagingDefaults.GenresPageSize, cancellationToken);
    }
}