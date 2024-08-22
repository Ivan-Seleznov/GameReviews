using GameReviews.Application.Common.Interfaces.Repositories;
using GameReviews.Domain.Entities.User;
using GameReviews.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameReviews.Infrastructure.Repositories;

internal sealed class UsersRepository(ApplicationDbContext context)
    : Repository<UserEntity, UserId>(context), IUsersRepository
{
    public async Task<bool> IsEmailExistsAsync(string email)
    {
        return await context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<bool> IsUsernameExistsAsync(string userName)
    {
        return await context.Users.AnyAsync(u => u.Username == userName);
    }

    public async Task<UserEntity?> GetByUsernameAsync(string username)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<UserEntity?> GetByEmailAsync(string email)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}

