using Carter;
using GameReviews.Application.Users.Commands.CreateUser;
using GameReviews.Application.Users.Queries.GetUser;
using GameReviews.Domain.Entities.User;
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
            var result = await sender.Send(new GetUserQuery(new UserId(id)));
            if (result is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(result);
        }).WithName("GetUser");

        app.MapPost("/", async (CreateUserCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);
            return Results.CreatedAtRoute("GetUser", new { id = result.Id }, result); 
        });
    }
}

