using GameReviews.Domain.Entities.UserAggregate;
using GameReviews.Domain.Entities.UserAggregate.Entities;

namespace GameReviews.Application.Common.Interfaces;
public interface IUserIdStorage
{
    public UserId? UserId  { get; set; }
}
