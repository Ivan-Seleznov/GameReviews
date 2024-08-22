using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameReviews.Domain.Entities.RefreshToken;
using GameReviews.Domain.Entities.User;

namespace GameReviews.Application.Common;
public interface IRefreshTokenProvider
{
    RefreshTokenEntity GenerateToken(UserId userId);
}
