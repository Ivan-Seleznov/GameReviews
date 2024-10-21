using GameReviews.Application.Common;
using GameReviews.Application.Common.Interfaces.Query;
using GameReviews.Application.Common.Models.Dtos.Game;
using GameReviews.Application.Common.Models.Dtos.Image;
using GameReviews.Domain.Results;
using Igdb.Abstractions;
using Igdb.Abstractions.Extensions.Images;

namespace GameReviews.Application.Games.Queries.SearchGames;
internal class SearchGamesQueryHandler : IQueryHandler<SearchGamesQuery, PagedList<GameInfoDto>>
{
    private readonly IIgdbClient _idgbClient;
    public SearchGamesQueryHandler(IIgdbClient idgbClient)
    {
        _idgbClient = idgbClient;
    }
    public async Task<Result<PagedList<GameInfoDto>>> Handle(SearchGamesQuery request, CancellationToken cancellationToken)
    {
        var page = request.Page ?? 1;
        var pageSize = request.PageSize ?? 10;

        if (string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            return new PagedList<GameInfoDto>(new List<GameInfoDto>(), page, pageSize,0);
        }

        var gamesResponse = await _idgbClient.GameQueryService.SearchGame(request.SearchTerm,page,pageSize);

        
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

        return new PagedList<GameInfoDto>(gameDtos, page, pageSize, gamesResponse.Count);
    }
}
