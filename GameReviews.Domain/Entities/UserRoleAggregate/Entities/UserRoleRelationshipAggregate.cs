using GameReviews.Domain.Common.Abstractions.Entities;
using GameReviews.Domain.Entities.UserAggregate.Entities;

namespace GameReviews.Domain.Entities.UserRoleAggregate.Entities;

public class UserRoleRelationshipAggregate : IAggregateRoot
{
    public UserId UsersId { get; private set; }
    public int RolesId { get; private set; }

    internal UserRoleRelationshipAggregate(UserId usersId, int rolesId)
    {
        UsersId = usersId;
        RolesId = rolesId;
    }
}