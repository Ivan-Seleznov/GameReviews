using GameReviews.Domain.Common;
using GameReviews.Domain.Entities.User;

namespace GameReviews.Domain.Entities.Roles;

public sealed class Role : Enumeration<Role>
{
    public static readonly Role Registered = new(1, nameof(Registered));
    public static readonly Role Admin = new(2, nameof(Admin));
    public Role(int id, string name) : base(id, name)
    {
    }

    public ICollection<PermissionEntity> Permissions { get; set; }
    public ICollection<UserEntity> Users { get; set; }
}