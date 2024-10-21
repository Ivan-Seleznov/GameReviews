using GameReviews.Application.Common.Interfaces.Repositories;
using GameReviews.Domain.Entities.Game;
using GameReviews.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameReviews.Infrastructure.Repositories;
public class GamesRepository(ApplicationDbContext context) : Repository<GameEntity, GameId>(context), IGamesRepository
{
    public Task<List<GameEntity>> GetAllAsync(int page, int size, string? searchTerm)
    {
        IQueryable<GameEntity> gamesQuery = context.Games;
        if (!string.IsNullOrWhiteSpace(searchTerm)) 
        {
            gamesQuery.Where(g => g.Name.Contains(searchTerm));
        }

        return gamesQuery.ToListAsync();
    }
}