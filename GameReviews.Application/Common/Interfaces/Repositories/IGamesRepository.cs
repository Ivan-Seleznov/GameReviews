using GameReviews.Domain.Entities.Game;

namespace GameReviews.Application.Common.Interfaces.Repositories;
public interface IGamesRepository : IRepository<GameEntity,GameId>
{
    Task<List<GameEntity>> GetAllAsync(int page, int size, string? searchTerm);
}
