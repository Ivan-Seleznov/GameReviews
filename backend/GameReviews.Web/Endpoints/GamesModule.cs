using Carter;
using GameReviews.Application.Common;
using GameReviews.Application.Common.Models.Dtos.Game;
using GameReviews.Application.Games.Commands;
using GameReviews.Application.Games.Queries.GetGame;
using GameReviews.Application.Games.Queries.GetGames;
using GameReviews.Application.Games.Queries.SearchGames;
using GameReviews.Application.Users.Commands.AddGame;
using GameReviews.Domain.Common.Authorization;
using GameReviews.Domain.Entities.GameAggregate.Entities;
using GameReviews.Web.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

        app.MapGet("", async (
                [AsParameters] GameFilterParams filter,
                string? sortColumn,
                string? sortOrder,
                int? page,
                int? pageSize
                , ISender sender) =>
            {
                return (await sender.Send(new GetGamesQuery(filter, sortColumn, sortOrder, page, pageSize)))
                    .OkOrProblemDetails();
            })
            .Produces<PagedList<GameInfoDto>>();

        app.MapGet("/search", async (string searchTerm, int? page, int? pageSize, ISender sender) =>
            {
                return (await sender.Send(new SearchGamesQuery(searchTerm, page, pageSize)))
                    .OkOrProblemDetails();
            })
            .Produces<PagedList<GameInfoDto>>();

        app.MapPost("/", async (AddGameToUserCommand addGameToUserCommand, ISender sender) =>
            {
                return (await sender.Send(addGameToUserCommand))
                    .OkOrProblemDetails();
            })
            .Produces<GameInfoDto>();

        app.MapPut("/", async (UpdateGameCommand command, ISender sender) =>
            {
                return (await sender.Send(command))
                    .WithProblemDetails(x => Results.Ok(x)!);
            })
            .Produces<GameDetailsDto>()
            .RequireAuthorization(Permission.ManageUser.ToString());
    }
}