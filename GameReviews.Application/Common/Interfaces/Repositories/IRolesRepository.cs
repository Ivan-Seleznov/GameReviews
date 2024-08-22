using GameReviews.Domain.Entities.Roles;

namespace GameReviews.Application.Common.Interfaces.Repositories;
public interface IRolesRepository : IRepository<Role,int>
{
    Task<Role?> GetByNameAsync(string value);
}