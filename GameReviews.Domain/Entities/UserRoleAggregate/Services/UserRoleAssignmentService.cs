using GameReviews.Domain.Common.Abstractions.Repositories;
using GameReviews.Domain.Common.Abstractions.Services;
using GameReviews.Domain.Common.Errors;
using GameReviews.Domain.Entities.UserAggregate.Entities;
using GameReviews.Domain.Entities.UserRoleAggregate.Entities;
using GameReviews.Domain.Results;

namespace GameReviews.Domain.Entities.UserRoleAggregate.Services;

internal sealed class UserRoleAssignmentService : IUserRoleAssignmentService
{
    private readonly IRolesRepository _rolesRepository;
    
    public UserRoleAssignmentService(IRolesRepository rolesRepository)
    {
        _rolesRepository = rolesRepository;
    }
    
    public async Task<Result> AssignRoleToUserAsync(UserId userId, string roleName)
    {
        var role = await _rolesRepository.GetByNameAsync(roleName);
        if (role is null)
        {
            return Result.Failure(UserErrors.RoleNotExist(roleName));
        }
        
        await _rolesRepository.AddUserRoleRelationshipAsync(new UserRoleRelationshipAggregate(userId, role.Id));
        return Result.Success();
    }
}