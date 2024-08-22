﻿using GameReviews.Domain.Common;
using GameReviews.Domain.Entities.Roles;
using GameReviews.Domain.Entities.RefreshToken;


namespace GameReviews.Domain.Entities.User;

public class UserEntity : BaseDomainEntity<UserId>
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public ICollection<RefreshTokenEntity> RefreshTokens { get; set; }

    //public ICollection<GameEntity> Games { get; set; }

    public ICollection<Role> Roles { get; set; }
}

