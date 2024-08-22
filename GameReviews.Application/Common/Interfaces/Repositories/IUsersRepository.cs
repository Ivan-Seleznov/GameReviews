using GameReviews.Domain.Entities.User;

namespace GameReviews.Application.Common.Interfaces.Repositories
{
    public interface IUsersRepository : IRepository<UserEntity, UserId>
    {
        Task<bool> IsEmailExistsAsync(string email);
        Task<bool> IsUsernameExistsAsync(string username);

        Task<UserEntity?> GetByUsernameAsync(string username);
        Task<UserEntity?> GetByEmailAsync(string email);
    }
}