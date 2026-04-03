using GameReviews.Domain.Common.Abstractions.Entities;
using GameReviews.Domain.Common.Authorization;
using GameReviews.Domain.Entities.PermissionAggregate.Entities;
using GameReviews.Domain.Entities.RolesAggregate.Entities;

namespace GameReviews.Domain.Entities.RolePermissionAggregate.Entities;
public class RolePermission : IAggregateRoot
{
    public int RolesId { get; private set; }
    public int PermissionsId { get; private set; }

    private RolePermission() { }

    private RolePermission(int rolesId, int permissionsId)
    {
        RolesId = rolesId;
        PermissionsId = permissionsId;
    }
    public static RolePermission Create(Role role, Permission permission)
    {
        return new RolePermission(role.Id, (int)permission);
    }
}