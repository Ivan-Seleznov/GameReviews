using GameReviews.Application.Common.Interfaces.Command;
using GameReviews.Application.Common.Models.Dtos.User;

namespace GameReviews.Application.Users.Commands.RegisterUser;
public record RegisterUserCommand(
    string Username,
    string Email,
    string Password) : ICommand<AuthUserDto>;