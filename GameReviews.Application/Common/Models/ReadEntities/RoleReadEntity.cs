namespace GameReviews.Application.Common.Models.ReadEntities;
public sealed class RoleReadEntity
{
    public int Id { get; set; }
    public ICollection<PermissionReadEntity> Permissions { get; set; }
    public ICollection<UserReadEntity> Users { get; set; }
}