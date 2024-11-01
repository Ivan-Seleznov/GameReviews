using GameReviews.Domain.Entities.UserAggregate.Entities;

namespace GameReviews.Application.Common.Models.ReadEntities;

public class UserRoleReadEntity
{
    public int RolesId { get; set; }
    public UserId UsersId { get; set; }
    
    public UserReadEntity User { get; set; }
    public RoleReadEntity Role { get; set; }
}