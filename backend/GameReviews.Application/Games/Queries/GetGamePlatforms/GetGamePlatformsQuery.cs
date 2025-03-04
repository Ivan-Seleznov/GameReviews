using GameReviews.Application.Common.Interfaces.Query;
using GameReviews.Application.Common.Models.Dtos.Platform;
using GameReviews.Application.Common.PagedList;

namespace GameReviews.Application.Games.Queries.GetGamePlatforms;

public record GetGamePlatformsQuery(
    string? SortColumn, 
    string? SortOrder,
    int? Page, 
    int? PageSize) : IQuery<PagedList<PlatformDto>>;