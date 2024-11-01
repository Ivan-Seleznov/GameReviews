using GameReviews.Domain.Common;
using GameReviews.Domain.Common.Abstractions.Entities;
using GameReviews.Domain.Common.Authorization;
using GameReviews.Domain.Entities.PermissionAggregate;
using GameReviews.Domain.Entities.PermissionAggregate.Entities;
using GameReviews.Domain.Entities.UserAggregate.Entities;

namespace GameReviews.Domain.Entities.RolesAggregate.Entities;

public sealed class Role : Enumeration<Role>, IAggregateRoot
{
    public static readonly Role Registered = Create(1, nameof(Registered));
    public static readonly Role Admin = Create(2, nameof(Admin));
    
    private Role(int id, string name) : base(id, name)
    {
    }

    public static Role Create(int id, string name)
    {
        return new Role(id,name);
    }
}