using GameReviews.Domain.Common.Abstractions.Repositories;
using GameReviews.Domain.Entities.GameAggregate.Entities;
using GameReviews.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameReviews.Infrastructure.Repositories;
public class GamesRepository(ApplicationWriteDbContext context) : Repository<GameEntity, GameId>(context), IGamesRepository
{
}