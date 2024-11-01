using GameReviews.Application.Common;
using GameReviews.Application.Common.Interfaces.Query;
using GameReviews.Application.Common.Models.Dtos.Game;

namespace GameReviews.Application.Games.Queries.GetGames;

public record GameFilterParams(
    string? Category,
    string? Status,
    long[]? Platforms,
    int? StartYear,
    int? EndYear);
public record GetGamesQuery(
    GameFilterParams Filter,
    string? SortColumn, 
    string? SortOrder,
    int? Page, 
    int? PageSize) : IQuery<PagedList<GameInfoDto>>;