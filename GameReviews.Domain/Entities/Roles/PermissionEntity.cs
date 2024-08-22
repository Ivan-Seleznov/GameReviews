using GameReviews.Domain.Common;

namespace GameReviews.Domain.Entities.Roles;
public class PermissionEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}