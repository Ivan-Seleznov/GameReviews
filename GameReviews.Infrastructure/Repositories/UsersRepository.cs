using GameReviews.Application.Users.Repository;
using GameReviews.Domain.Entities.User;
using GameReviews.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameReviews.Infrastructure.Repositories;

internal sealed class UsersRepository(ApplicationDbContext context)
    : Repository<UserEntity, UserId>(context), IUsersRepository
{
    public async Task<bool> IsEmailUniqueAsync(string email)
    {
        return !(await context.Users.AnyAsync(u => u.Email == email));
    }

    public async Task<bool> IsUsernameUniqueAsync(string userName)
    {
        return !(await context.Users.AnyAsync(u => u.Username == userName));
    }
}

