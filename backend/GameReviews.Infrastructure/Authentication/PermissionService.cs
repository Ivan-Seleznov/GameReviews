using GameReviews.Application.Common.Models.ReadEntities;
using GameReviews.Domain.Entities.UserAggregate;
using GameReviews.Domain.Entities.UserAggregate.Entities;
using GameReviews.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameReviews.Infrastructure.Authentication;
public class PermissionService : IPermissionService
{
    private readonly ApplicationReadDbContext _context;
    public PermissionService(ApplicationReadDbContext context)
    {
        _context = context;
    }

    public async Task<HashSet<string>> GetPermissionsAsync(UserId userId)
    {
        var roles = await _context.Set<UserRoleReadEntity>()
            .Where(ur => ur.UsersId == userId)
            .SelectMany(ur => ur.Role.Permissions)
            .Select(p => p.Name)
            .Distinct()
            .ToListAsync();

        return roles.ToHashSet();
    }
}
