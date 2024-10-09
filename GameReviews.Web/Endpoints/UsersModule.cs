using Carter;
using GameReviews.Application.Common.Models.Dtos.User;
using GameReviews.Application.Users.Commands.AddGame;
using GameReviews.Application.Users.Commands.CreateUser;
using GameReviews.Application.Users.Queries.GetUser;
using GameReviews.Domain.Common.Authorization;
using GameReviews.Domain.Common.Result;
using GameReviews.Domain.Entities.Game;
using GameReviews.Domain.Entities.User;
using GameReviews.Web.Extensions;
using MediatR;

namespace GameReviews.Web.Endpoints;

public class UsersModule : CarterModule
{
    private const string UsersBasePath = "/users";

    public UsersModule() : base(UsersBasePath)
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", async (int id, ISender sender) =>
            {
                return (await sender.Send(new GetUserQuery(new UserId(id))))
                    .WithProblemDetails(x => Results.Ok(x));
            })
            .WithName("GetUser")
            .Produces<UserDetailsDto>()
            .RequireAuthorization(new[] { Permission.ReadUser.ToString() });

        app.MapPost("/", async (CreateUserCommand command, ISender sender) =>
            {
                return (await sender.Send(command))
                    .WithProblemDetails(x =>
                        Results.CreatedAtRoute(
                            "GetUser",
                            new { id = x.Id }, x));
            })
            .Produces<UserDetailsDto>(StatusCodes.Status201Created)
            .RequireAuthorization(new[] { Permission.ManageUser.ToString() });


        var gamesGroup = app.MapGroup("/games")
            .RequireAuthorization(new[] { Permission.ReadUser.ToString() });

        gamesGroup.MapPost("/", async (AddGameToUserCommand adGameToUserCommand, ISender sender) =>
        {
            var result = await sender.Send(adGameToUserCommand);
            return result.WithProblemDetails(x => Results.Ok(x));
        });
    }
}