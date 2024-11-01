using GameReviews.Domain.Common.Abstractions.Entities;
using GameReviews.Domain.Entities.GameAggregate.Entities;
using GameReviews.Domain.Entities.UserAggregate.Entities;

namespace GameReviews.Domain.Entities.UserGameRelationAggregate.Entities;
public class GameUserRelationship : IAggregateRoot
{
    //Add one class for id
    public GameId GamesId { get; private set; } 
    public UserId UsersId { get; private set; }
    private GameUserRelationship() { }
    internal GameUserRelationship(GameId gameId, UserId userId)
    {
        GamesId = gameId;
        UsersId = userId;
    }
}