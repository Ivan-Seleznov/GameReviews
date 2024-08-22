using GameReviews.Application.Common.Interfaces.Command;
using GameReviews.Application.Common.Models.Dtos.User;
using GameReviews.Domain.Entities.Roles;

namespace GameReviews.Application.Users.Commands.CreateUser;

public record CreateUserCommand(
    string Username,
    string Email,
    string Password,
    string? RoleName = null) : ICommand<UserDetailsDto>;