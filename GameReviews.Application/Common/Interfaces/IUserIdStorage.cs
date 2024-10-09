using GameReviews.Domain.Entities.User;

namespace GameReviews.Application.Common.Interfaces;
public interface IUserIdStorage
{
    public UserId? UserId  { get; set; }
}
