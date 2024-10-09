using GameReviews.Domain.Entities.Game;

namespace GameReviews.Application.Common.Interfaces.Repositories;
public interface IGamesRepository : IRepository<GameEntity,GameId>
{
}
