using Carter;
using GameReviews.Application.Common.Models.Dtos.Game;
using GameReviews.Application.Games.Commands;
using GameReviews.Application.Games.Queries.GetGame;
using GameReviews.Application.Games.Queries.SearchGames;
using GameReviews.Domain.Common.Authorization;
using GameReviews.Domain.Entities.Game;
using GameReviews.Web.Extensions;
using MediatR;

namespace GameReviews.Web.Endpoints;

public class GamesModule : CarterModule
{
    private const string GamesBasePath = "/games";

    public GamesModule() : base(GamesBasePath)
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", async (long id, ISender sender) =>
        {
            return (await sender.Send(new GetGameQuery(new GameId(id))))
                .WithProblemDetails(x => Results.Ok(x)!);
        });

        app.MapPut("/", async (UpdateGameCommand command, ISender sender) =>
            {
                return (await sender.Send(command))
                    .WithProblemDetails(x => Results.Ok(x)!);
            })
            .Produces<GameDetailsDto>()
            .RequireAuthorization(new[] { Permission.ManageUser.ToString() });

        app.MapGet("/search", async (string searchTerm, int? page, int? pageSize, ISender sender) =>
        {
            return (await sender.Send(new SearchGamesQuery(searchTerm, page, pageSize)))
                .WithProblemDetails(x => Results.Ok(x)!);
        })
        .Produces<List<GameDetailsDto>>();
    }
}