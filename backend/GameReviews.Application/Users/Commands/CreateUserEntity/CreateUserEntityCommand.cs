using GameReviews.Application.Common.Interfaces.Command;
using GameReviews.Domain.Entities.UserAggregate.Entities;

namespace GameReviews.Application.Users.Commands.CreateUserEntity;

/// <summary>
/// Command to create a new user entity in the system. This command is used internally and returns a 
/// <see cref="UserEntity"/> directly.
/// </summary>
internal record CreateUserEntityCommand(
    string Username,
    string Email,
    string Password,
    string? RoleName = null) : ICommand<UserEntity>;