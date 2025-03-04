using Carter;
using GameReviews.Application.Common;
using GameReviews.Application.Common.Models.Dtos.Review;
using GameReviews.Application.Reviews.Commands.CreateReview;
using GameReviews.Application.Reviews.Queries.GetUserReviews;
using GameReviews.Domain.Common.Authorization;
using GameReviews.Web.Extensions;
using MediatR;

namespace GameReviews.Web.Endpoints;
public class ReviewsModule : CarterModule
{
    private const string ReviewsBasePath = "/reviews";
    public ReviewsModule() : base(ReviewsBasePath)
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (CreateReviewCommand command, ISender sender) => 
                (await sender.Send(command)).OkOrProblemDetails())
            .RequireAuthorization(new[] { Permission.ReadUser.ToString() })
            .Produces<ReviewDetailsDto>();
    }
}