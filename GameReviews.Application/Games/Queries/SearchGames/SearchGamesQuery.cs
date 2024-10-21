using GameReviews.Application.Common;
using GameReviews.Application.Common.Interfaces.Query;
using GameReviews.Application.Common.Models.Dtos.Game;

namespace GameReviews.Application.Games.Queries.SearchGames;
public record SearchGamesQuery(string SearchTerm, int? Page, int? PageSize) : IQuery<PagedList<GameInfoDto>>;