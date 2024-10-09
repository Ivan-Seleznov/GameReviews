using GameReviews.Application.Common.Errors;
using GameReviews.Application.Common.Interfaces.Query;
using GameReviews.Application.Common.Models.Dtos.Game;
using GameReviews.Domain.Common.Result;
using Igdb.Abstractions;

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
        };
    }
}