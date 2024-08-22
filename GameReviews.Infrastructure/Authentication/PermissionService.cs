using GameReviews.Domain.Entities.User;
using GameReviews.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameReviews.Infrastructure.Authentication;
public class PermissionService : IPermissionService
{
    private readonly ApplicationDbContext _context;
    public PermissionService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<HashSet<string>> GetPermissionsAsync(UserId userId)
    {
        var roles = await _context.Users
            .Include(u => u.Roles)
            .ThenInclude(r => r.Permissions)
            .Where(u => u.Id == userId)
            .Select(u => u.Roles)
            .ToArrayAsync();

        return roles
            .SelectMany(r => r)
            .SelectMany(r => r.Permissions)
            .Select(p => p.Name)
            .ToHashSet();
    }
}
