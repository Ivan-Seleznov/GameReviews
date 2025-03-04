using GameReviews.Application.Common.Interfaces.Query;
using GameReviews.Application.Common.Models.Dtos.Genre;
using GameReviews.Application.Common.PagedList;

namespace GameReviews.Application.Games.Queries.GetGameGenres;

public record GetGameGenresQuery(
    string? SortColumn, 
    string? SortOrder,
    int? Page, 
    int? PageSize) : IQuery<PagedList<GenreDto>>;