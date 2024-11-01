using GameReviews.Domain.Common.Abstractions.Entities;
using GameReviews.Domain.Entities.UserAggregate.Entities;

namespace GameReviews.Domain.Entities.GameAggregate.Entities;
public class GameEntity : BaseEntity<GameId>, IAggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    
    private GameEntity() { }
    private GameEntity(GameId gameId,string name, string description)
    {
        Name = name;
        Description = description;
        Id = gameId;
    } 
    public static GameEntity Create(GameId gameId,string name, string description)
    {
        return new GameEntity(gameId,name, description);
    }

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
public record GameId(long Value) : EntityId<long>(Value);