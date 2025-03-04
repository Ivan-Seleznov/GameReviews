using GameReviews.Application.Common.Constants;
using GameReviews.Application.Common.Interfaces;
using GameReviews.Application.Common.Interfaces.Query;
using GameReviews.Application.Common.Models.Dtos.Genre;
using GameReviews.Application.Common.Models.Dtos.Platform;
using GameReviews.Application.Common.PagedList;
using GameReviews.Application.Games.Queries.GetGameGenres;
using GameReviews.Domain.Results;

namespace GameReviews.Application.Games.Queries.GetGamePlatforms;

public class GetGamePlatformsQueryHandler : IQueryHandler<GetGamePlatformsQuery,PagedList<PlatformDto>>
{
    private readonly IGameDetailsService _gameDetailsService;
    public GetGamePlatformsQueryHandler(IGameDetailsService gameDetailsService)
    {
        _gameDetailsService = gameDetailsService;
    }
    
    public async Task<Result<PagedList<PlatformDto>>> Handle(GetGamePlatformsQuery request, CancellationToken cancellationToken)
    {
        return await _gameDetailsService.GetPlatformsAsync(request.SortColumn ?? "abbreviation", request.SortOrder ?? "desc",request.Page ?? 1, request.PageSize ?? PagingDefaults.PlatformsPageSize, cancellationToken);
    }
}