using GameReviews.Domain.Entities.RolesAggregate.Entities;
using GameReviews.Domain.Entities.UserRoleAggregate.Entities;

namespace GameReviews.Domain.Common.Abstractions.Repositories;
public interface IRolesRepository : IRepository<Role,int>
{
    Task<Role?> GetByNameAsync(string value);
    Task AddUserRoleRelationshipAsync(UserRoleRelationshipAggregate userRoleRelationship);
}