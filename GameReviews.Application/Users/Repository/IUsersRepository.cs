using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameReviews.Application.Common.Interfaces;
using GameReviews.Domain.Entities.User;

namespace GameReviews.Application.Users.Repository
{
    public interface IUsersRepository : IRepository<UserEntity,UserId>
    {
        Task<bool> IsEmailUniqueAsync(string email);
        Task<bool> IsUsernameUniqueAsync(string userName);
    }
}
