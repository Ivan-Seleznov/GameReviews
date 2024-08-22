using GameReviews.Domain.Common;
using GameReviews.Domain.Common.Authorization;

namespace GameReviews.Domain.Entities.Roles;
public class RolePermission
{
    public int RoleId { get; set; }
    public int PermissionsId { get; set; }


    public static RolePermission Create(Role role, Permission permission)
    {
        return new RolePermission
        {
            RoleId = role.Id,
            PermissionsId = (int)permission
        };
    }
}
