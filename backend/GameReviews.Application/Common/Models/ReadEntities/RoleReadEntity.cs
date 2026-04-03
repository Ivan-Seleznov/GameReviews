namespace GameReviews.Application.Common.Models.ReadEntities;
public sealed class RoleReadEntity
{
    public int Id { get; set; }
    public ICollection<PermissionReadEntity> Permissions { get; set; }
}