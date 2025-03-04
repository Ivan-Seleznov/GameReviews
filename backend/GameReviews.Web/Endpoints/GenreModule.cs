using Carter;
using GameReviews.Application.Common.Models.Dtos.Genre;
using GameReviews.Application.Common.PagedList;
using GameReviews.Application.Games.Queries.GetGameGenres;
using GameReviews.Web.Extensions;
using MediatR;

namespace GameReviews.Web.Endpoints;

public class GenreModule : CarterModule
{
    private const string PlatformsBasePath = "/genres";

    public GenreModule() : base(PlatformsBasePath)
    {
    }
    
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/game", async ([AsParameters]GetGameGenresQuery query, ISender sender) 
            => (await sender.Send(query)).OkOrProblemDetails())
            .Produces<PagedList<GenreDto>>();
    }
}