using GameReviews.Domain.Entities.GameAggregate.Entities;
using GameReviews.Domain.Entities.UserAggregate.Entities;

namespace GameReviews.Application.Common.Models.ReadEntities;
public class GameUserReadEntity
{
    public GameId GamesId { get; set; }
    public UserId UsersId { get; set; }
}