using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameReviews.Application.Common;
using GameReviews.Application.Common.Constants;
using GameReviews.Application.Common.Interfaces;
using GameReviews.Application.Common.Interfaces.Query;
using GameReviews.Application.Common.Models.Dtos.Game;
using GameReviews.Application.Common.Models.ReadEntities;
using GameReviews.Application.Common.PagedList;
using GameReviews.Domain.Results;
using Microsoft.EntityFrameworkCore;

namespace GameReviews.Application.Games.Queries.GetUserGames;

internal class GetUserGamesQueryHandler : IQueryHandler<GetUserGamesQuery, PagedList<GameInfoDto>>
{
    private readonly IReadApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUserIdStorage _userIdStorage;

    public GetUserGamesQueryHandler(IReadApplicationDbContext context, IMapper mapper, IUserIdStorage userIdStorage)
    {
        _context = context;
        _mapper = mapper;
        _userIdStorage = userIdStorage;
    }
    
    public async Task<Result<PagedList<GameInfoDto>>> Handle(GetUserGamesQuery request, CancellationToken cancellationToken)
    {
        var userId = _userIdStorage.UserId!;
        
        IQueryable<GameReadEntity> query = _context.Games.Where(g => g.Users.Any(u => u.Id == userId));
        if (request.SearchTerm is not null)
        {
            query = query.Where(g => g.Name.Contains(request.SearchTerm));
        }
        
        var keySelector = GetSortProperty(request.SortColumn);
        query = request.SortOrder?.ToLower() == "desc" ? query.OrderByDescending(keySelector) : query.OrderBy(keySelector);
        
        return await PagedList<GameInfoDto>.CreateWithQueryAsync(
            query.ProjectTo<GameInfoDto>(_mapper.ConfigurationProvider),
            request.Page ?? 1,
            request.PageSize ?? PagingDefaults.FilterPageSize);
    }
    
    private static Expression<Func<GameReadEntity, object>> GetSortProperty(string? sortColumn)
    {
        return sortColumn?.ToLower() switch
        {
            "id" => game => game.Id,
            _ => game => game.Name
            //TODO: add more columns
        };
    }
}