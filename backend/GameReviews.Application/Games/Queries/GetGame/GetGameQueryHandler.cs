using GameReviews.Application.Common.Errors;
using GameReviews.Application.Common.Interfaces;
using GameReviews.Application.Common.Interfaces.Query;
using GameReviews.Application.Common.Models.Dtos.Game;
using GameReviews.Domain.Results;

namespace GameReviews.Application.Games.Queries.GetGame;
internal class GetGameQueryHandler : IQueryHandler<GetGameQuery,GameDetailsDto>
{
    private readonly IGameDetailsService _gameDetailsService;
    public GetGameQueryHandler(IGameDetailsService gameDetailsService)
    {
        _gameDetailsService = gameDetailsService;
    }
    public async Task<Result<GameDetailsDto>> Handle(GetGameQuery request, CancellationToken cancellationToken)
    {
        var game = await _gameDetailsService.GetGameByIdAsync(request.Id.Value, cancellationToken);
        if (game is null)
        {
            return GameErrors.NotFound(request.Id);
        }

        return game;
    }
}