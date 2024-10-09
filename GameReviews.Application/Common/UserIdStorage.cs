using GameReviews.Application.Common.Interfaces;
using GameReviews.Domain.Entities.User;

namespace GameReviews.Application.Common;

public class UserIdStorage : IUserIdStorage
{
    public UserId? UserId { get; set; }
}