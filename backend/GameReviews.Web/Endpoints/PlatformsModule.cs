using Carter;
using GameReviews.Application.Common.Models.Dtos.Platform;
using GameReviews.Application.Common.PagedList;
using GameReviews.Application.Games.Queries.GetGamePlatforms;
using GameReviews.Web.Extensions;
using MediatR;

namespace GameReviews.Web.Endpoints;

public class PlatformsModule : CarterModule
{
    private const string PlatformsBasePath = "/platforms";

    public PlatformsModule() : base(PlatformsBasePath)
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/game", async ([AsParameters]GetGamePlatformsQuery query, ISender sender) 
                => (await sender.Send(query)).OkOrProblemDetails())
            .Produces<PagedList<PlatformDto>>();
    }
}