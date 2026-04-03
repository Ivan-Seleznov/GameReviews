using Microsoft.AspNetCore.Mvc;

namespace GameReviews.Web.Common.Requests;

public class GetReviewsByGameRequest
{
    [FromRoute] public long GameId { get; set; }
    [FromQuery] public string? SortOrder { get; set; }
    [FromQuery] public int? Page { get; set; }
    [FromQuery] public int? PageSize { get; set; }
}
