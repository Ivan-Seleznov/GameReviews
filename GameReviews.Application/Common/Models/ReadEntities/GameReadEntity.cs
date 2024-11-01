using GameReviews.Domain.Entities.GameAggregate.Entities;

namespace GameReviews.Application.Common.Models.ReadEntities;
public class GameReadEntity
{
    public GameId Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<UserReadEntity> Users { get; set; }
}
