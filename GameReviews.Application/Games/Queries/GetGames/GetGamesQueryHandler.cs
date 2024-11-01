using GameReviews.Application.Common;
using GameReviews.Application.Common.Constants;
using GameReviews.Application.Common.Interfaces.Query;
using GameReviews.Application.Common.Models.Dtos.Game;
using GameReviews.Application.Common.Models.Dtos.Image;
using GameReviews.Domain.Results;
using Idgb.Abstractions.General.Enumerations;
using Igdb.Abstractions;
using Igdb.Abstractions.Common.Filters;
using Igdb.Abstractions.Extensions.Images;
using Igdb.Abstractions.Models.Games;

namespace GameReviews.Application.Games.Queries.GetGames;

internal class GetGamesQueryHandler : IQueryHandler<GetGamesQuery, PagedList<GameInfoDto>>
{
    private readonly IIgdbClient _idgbClient;

    public GetGamesQueryHandler(IIgdbClient idgbClient)
    {
        _idgbClient = idgbClient;
    }
    
    public async Task<Result<PagedList<GameInfoDto>>> Handle(GetGamesQuery request, CancellationToken cancellationToken)
    {
        var page = request.Page ?? 1;
        var pageSize = request.PageSize ?? PagingDefaults.FilterPageSize;
        
        var gameFilter = new GameFilter
        {
            Category = Enum.TryParse(request.Filter.Category, out Category category) ? category : null,
            Status = Enum.TryParse(request.Filter.Status, out GameStatus status) ? status : null,
            EndYear = request.Filter.EndYear,
            StartYear = request.Filter.StartYear,
            PlatformIds = request.Filter.Platforms,
        };
        
         var gamesResponse = await _idgbClient.GameQueryService.GetAllGamesAsync(
             gameFilter,
             request.SortColumn ?? "name",
             request.SortOrder is not null && request.SortOrder == "asc" ? SortOrder.Asc : SortOrder.Desc,
             page,
             pageSize);
         
         var gameDtos = gamesResponse.Data.Select((game) => new GameInfoDto
         {
             Id = game!.Id,
             Name = game.Name,
             Description = game.Summary,
             Cover = game.Cover != null ? new ImageDto
             {
                 Width = game.Cover.Value.Width,
                 Height = game.Cover.Value.Height,
                 Url = game.Cover.Value.GetUrlWithPixelCount(720)
             } : null,
         }).ToList();
         
         return PagedList<GameInfoDto>.Create(gameDtos, page, pageSize, gamesResponse.Count);
    }
}