using GameReviews.Domain.Common.Abstractions.Entities;
using GameReviews.Domain.Common.Authorization;
using GameReviews.Domain.Entities.PermissionAggregate.Entities;
using GameReviews.Domain.Entities.RolesAggregate.Entities;

namespace GameReviews.Domain.Entities.RolePermissionAggregate.Entities;
public class RolePermission : IAggregateRoot
{
    public int RoleId { get; private set; }
    public int PermissionEntityId { get; private set; }

    private RolePermission() { }

    private RolePermission(int roleId, int permissionsId)
    {
        RoleId = roleId;
        PermissionEntityId = permissionsId;
    }
    public static RolePermission Create(Role role, Permission permission)
    {
        return new RolePermission(role.Id, (int)permission);
    }
}