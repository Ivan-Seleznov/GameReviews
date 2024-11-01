using GameReviews.Domain.Common.Authorization;

namespace GameReviews.Application.Common.Models.ReadEntities;
public class RolePermissionReadEntity
{
    public int RoleId { get; set; }
    public int PermissionsId { get; set; }
}