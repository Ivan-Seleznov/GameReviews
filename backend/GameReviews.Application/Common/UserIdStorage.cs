using GameReviews.Application.Common.Interfaces;
using GameReviews.Domain.Entities.UserAggregate;
using GameReviews.Domain.Entities.UserAggregate.Entities;

namespace GameReviews.Application.Common;

public class UserIdStorage : IUserIdStorage
{
    public UserId? UserId { get; set; }
}