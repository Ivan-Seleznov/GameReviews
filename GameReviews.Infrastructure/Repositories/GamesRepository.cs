using GameReviews.Application.Common.Interfaces.Repositories;
using GameReviews.Domain.Entities.Game;
using GameReviews.Infrastructure.Data;

namespace GameReviews.Infrastructure.Repositories;
public class GamesRepository(ApplicationDbContext context) : Repository<GameEntity, GameId>(context), IGamesRepository
{
}