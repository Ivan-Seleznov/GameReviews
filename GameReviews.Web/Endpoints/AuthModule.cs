using Carter;
using GameReviews.Application.Users.Commands.LoginUser;
using GameReviews.Application.Users.Commands.RegisterUser;
using GameReviews.Application.Users.Commands.RefreshUserToken;
using MediatR;

namespace GameReviews.Web.Endpoints;

public class AuthModule : CarterModule
{
    private const string AuthBasePath = "/auth";
    public AuthModule() : base(AuthBasePath)
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/login", async (LoginUserCommand loginCommand, CancellationToken cancellationToken, ISender sender) =>
        {
            return Results.Ok(await sender.Send(loginCommand, cancellationToken));
        });

        app.MapPost("/register", async (RegisterUserCommand registerCommand, CancellationToken cancellationToken, ISender sender) =>
        {
            return Results.Ok(await sender.Send(registerCommand, cancellationToken));
        });

        app.MapPost("/refresh", async (RefreshUserTokenCommand refreshUserTokenCommand, CancellationToken cancellationToken, ISender sender) =>
        {
            return Results.Ok(await sender.Send(refreshUserTokenCommand, cancellationToken));
        });
    }
}