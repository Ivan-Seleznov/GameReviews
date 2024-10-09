using GameReviews.Domain.Entities.Game;
using GameReviews.Domain.Entities.User;

namespace GameReviews.Domain.Entities.UserGame;
public class GameEntityUserEntity
{
    public GameId GamesId { get; set; }
    public UserId UsersId { get; set; }

}