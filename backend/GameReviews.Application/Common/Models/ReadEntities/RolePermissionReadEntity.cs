using GameReviews.Domain.Common.Authorization;

namespace GameReviews.Application.Common.Models.ReadEntities;
public class RolePermissionReadEntity
{
    public int RolesId { get; set; }
    public int PermissionsId { get; set; }
}