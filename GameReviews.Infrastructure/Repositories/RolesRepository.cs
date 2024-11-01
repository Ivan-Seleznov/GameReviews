using GameReviews.Domain.Common.Abstractions.Repositories;
using GameReviews.Domain.Entities.RolesAggregate.Entities;
using GameReviews.Domain.Entities.UserRoleAggregate.Entities;
using GameReviews.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameReviews.Infrastructure.Repositories;
internal class RolesRepository(ApplicationWriteDbContext context) : Repository<Role,int>(context), IRolesRepository 
{
    public async Task<Role?> GetByNameAsync(string value)
    {
        return await context.Set<Role>().FirstOrDefaultAsync(x => x.Name == value);
    }

    public async Task AddUserRoleRelationshipAsync(UserRoleRelationshipAggregate userRoleRelationship)
    {
        await context.Set<UserRoleRelationshipAggregate>().AddAsync(userRoleRelationship);
    }
}