using GameReviews.Application.Common;
using GameReviews.Application.Common.Interfaces.Query;
using GameReviews.Application.Common.Models.Dtos.Game;
using GameReviews.Application.Common.PagedList;

namespace GameReviews.Application.Games.Queries.GetUserGames;
public record GetUserGamesQuery(    
    string? SearchTerm, 
    string? SortColumn, 
    string? SortOrder,
    int? Page, 
    int? PageSize) : IQuery<PagedList<GameInfoDto>>;