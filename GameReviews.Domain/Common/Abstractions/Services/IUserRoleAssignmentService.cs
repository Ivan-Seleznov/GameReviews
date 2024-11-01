using GameReviews.Domain.Entities.UserAggregate.Entities;
using GameReviews.Domain.Results;

namespace GameReviews.Domain.Common.Abstractions.Services;

public interface IUserRoleAssignmentService
{
    public Task<Result> AssignRoleToUserAsync(UserId userId, string roleName);
}