using GameReviews.Domain.Common;

namespace GameReviews.Domain.Entities.User;

public class UserEntity : BaseEntity<UserId>
{
    public string Username { get; set; }
    public string Email { get; set; }

    //public ICollection<GameEntity> Games { get; set; }
}

