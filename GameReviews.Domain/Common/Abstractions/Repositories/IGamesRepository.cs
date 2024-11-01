using GameReviews.Domain.Entities.GameAggregate.Entities;

namespace GameReviews.Domain.Common.Abstractions.Repositories;
public interface IGamesRepository : IRepository<GameEntity,GameId>
{ }
