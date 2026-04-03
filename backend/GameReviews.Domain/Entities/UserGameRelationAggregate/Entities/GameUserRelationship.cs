using GameReviews.Domain.Common.Abstractions.Entities;
using GameReviews.Domain.Entities.GameAggregate.Entities;
using GameReviews.Domain.Entities.UserAggregate.Entities;

namespace GameReviews.Domain.Entities.UserGameRelationAggregate.Entities;
public class GameUserRelationship : IAggregateRoot
{
    public GameId GamesId { get; private set; } 
    public UserId UsersId { get; private set; }
    private GameUserRelationship() { }
    internal GameUserRelationship(GameId gamesId, UserId usersId)
    {
        GamesId = gamesId;
        UsersId = usersId;
    }
}