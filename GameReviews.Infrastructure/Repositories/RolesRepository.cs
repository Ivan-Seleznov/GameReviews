using GameReviews.Application.Common.Interfaces.Repositories;
using GameReviews.Domain.Entities.Roles;
using GameReviews.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameReviews.Infrastructure.Repositories;
internal class RolesRepository(ApplicationDbContext context) : Repository<Role,int>(context), IRolesRepository 
{
    public async Task<Role?> GetByNameAsync(string value)
    {
        return await context.Set<Role>().FirstOrDefaultAsync(x => x.Name == value);
    }
}