using GameReviews.Application.Common.Interfaces.Command;
using GameReviews.Application.Common.Models.Dtos.User;

namespace GameReviews.Application.Users.Commands.CreateUser;

public record CreateUserCommand(
    string Username,
    string Email) : ICommand<UserDetailsDto>;