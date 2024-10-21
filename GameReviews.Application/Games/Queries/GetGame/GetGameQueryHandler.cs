using GameReviews.Application.Common.Errors;
using GameReviews.Application.Common.Interfaces.Query;
using GameReviews.Application.Common.Models.Dtos.Game;
using GameReviews.Application.Common.Models.Dtos.Image;
using GameReviews.Domain.Common.Result;
using Igdb.Abstractions;
using Igdb.Abstractions.Extensions.Images;

namespace GameReviews.Application.Games.Queries.GetGame;
internal class GetGameQueryHandler : IQueryHandler<GetGameQuery,GameDetailsDto>
{
    private readonly IIgdbClient _idgbClient;
    public GetGameQueryHandler(IIgdbClient idgbClient)
    {
        _idgbClient = idgbClient;
    }
    public async Task<Result<GameDetailsDto>> Handle(GetGameQuery request, CancellationToken cancellationToken)
    {
        var game = await _idgbClient.GameQueryService.GetByIdAsync(request.Id.Value);
        if (game is null)
        {
            return GameErrors.NotFound(request.Id);
        }
        return new GameDetailsDto()
        {
            Id = game.Id,
            Name = game.Name,
            Description = game.Summary,
            ReleaseDate = game.FirstReleaseDate,
            Category = game.Category?.ToString(),
            GameStatus = game.Status?.ToString(),
            Cover = game.Cover?.Value != null ? new ImageDto
            {
                Height = game.Cover.Value.Height,
                Width = game.Cover.Value.Width,
                Url = game.Cover.Value.GetUrlWithPixelCount(),
            } : null,
            Screenshots = game.Screenshots?.Values.Select(s => new ImageDto
            {
                Height = s.Height,
                Width = s.Width,
                Url = s.GetUrlWithPixelCount()
            }).ToList(),
        };
    }
}