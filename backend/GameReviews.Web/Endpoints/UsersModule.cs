using Carter;
using GameReviews.Application.Common;
using GameReviews.Application.Common.Models.Dtos.Game;
using GameReviews.Application.Common.Models.Dtos.Review;
using GameReviews.Application.Common.Models.Dtos.User;
using GameReviews.Application.Common.PagedList;
using GameReviews.Application.Games.Queries.GetGames;
using GameReviews.Application.Games.Queries.GetUserGames;
using GameReviews.Application.Reviews.Queries.GetUserReviews;
using GameReviews.Application.Users.Commands.AddGame;
using GameReviews.Application.Users.Commands.CreateUser;
using GameReviews.Application.Users.Queries.GetUser;
using GameReviews.Domain.Common.Authorization;
using GameReviews.Domain.Entities.UserAggregate;
using GameReviews.Domain.Entities.UserAggregate.Entities;
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
            .RequireAuthorization(Permission.ReadUser.ToString());

        app.MapPost("/", async (CreateUserCommand command, ISender sender) =>
            {
                return (await sender.Send(command))
                    .WithProblemDetails(x =>
                        Results.CreatedAtRoute(
                            "GetUser",
                            new { id = x.Id }, x));
            })
            .Produces<UserDetailsDto>(StatusCodes.Status201Created)
            .RequireAuthorization(Permission.ManageUser.ToString());

        
        app.MapGet("/reviews", async ([AsParameters]GetUserReviewsQuery query, ISender sender) => 
            (await sender.Send(query)).OkOrProblemDetails())
            .RequireAuthorization(Permission.ReadUser.ToString())
            .Produces<PagedList<ReviewDetailsDto>>();
        
        app.MapGet("/games", async ([AsParameters]GetUserGamesQuery query, ISender sender) =>
            (await sender.Send(query)).OkOrProblemDetails())
            .RequireAuthorization(Permission.ReadUser.ToString())
            .Produces<PagedList<GameInfoDto>>();
    }
}