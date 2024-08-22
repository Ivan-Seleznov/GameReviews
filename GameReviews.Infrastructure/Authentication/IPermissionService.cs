using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameReviews.Domain.Entities.User;

namespace GameReviews.Infrastructure.Authentication;
internal interface IPermissionService
{
    Task<HashSet<string>> GetPermissionsAsync(UserId userId);
}
