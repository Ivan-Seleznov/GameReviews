using GameReviews.Domain.Entities.GameAggregate.Entities;
using GameReviews.Domain.Entities.UserAggregate.Entities;
using GameReviews.Domain.Results;

namespace GameReviews.Domain.Common.Abstractions.Services;

public interface IGameUserRelationshipService
{
    Task<Result> CreateRelationshipAsync(UserId userId, GameId gameId);
}