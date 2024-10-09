using Carter;
using GameReviews.Application.Common.Models.Dtos.Review;
using GameReviews.Application.Reviews.Commands.CreateReview;
using GameReviews.Domain.Common.Authorization;
using GameReviews.Web.Extensions;
using MediatR;

namespace GameReviews.Web.Endpoints;
public class ReviewsModule : CarterModule
{
    private const string UsersBasePath = "/reviews";
    public ReviewsModule() : base(UsersBasePath)
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (CreateReviewCommand command, ISender sender) =>
            {
                return (await sender.Send(command))
                    .WithProblemDetails(x => Results.Ok(x)!);
            })
            .RequireAuthorization(new[] { Permission.ReadUser.ToString() })
            .Produces<ReviewDetailsDto>();
    }
}