using GameReviews.Domain.Entities.Game;
using GameReviews.Domain.Entities.Review;
using GameReviews.Domain.Entities.User;
using GameReviews.Domain.Entities.UserGame;
using Microsoft.EntityFrameworkCore;

namespace GameReviews.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<UserEntity> Users { get; set; }
    DbSet<ReviewEntity> Reviews { get; set; }
    DbSet<GameEntity> Games { get; set; }
    DbSet<GameEntityUserEntity> UsersGames { get; set; }
}