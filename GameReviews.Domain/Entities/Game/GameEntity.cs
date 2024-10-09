using GameReviews.Domain.Common.Abstractions.Entities;
using GameReviews.Domain.Entities.User;

namespace GameReviews.Domain.Entities.Game;
public class GameEntity : BaseEntity<GameId>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<UserEntity> Users { get; set; }
}
