using GameReviews.Application.Common.Models.ReadEntities;
using Microsoft.EntityFrameworkCore;

namespace GameReviews.Application.Common.Interfaces;
public interface IReadApplicationDbContext
{
    DbSet<UserReadEntity> Users { get; set; }
    DbSet<ReviewReadEntity> Reviews { get; set; }
    DbSet<GameReadEntity> Games { get; set; }
    DbSet<GameUserReadEntity> UsersGames { get; set; }
}