using GameReviews.Application.Common.Interfaces.Command;
using GameReviews.Application.Common.Models.Dtos.User;

namespace GameReviews.Application.Users.Commands.LoginUser;

public record LoginUserCommand(
    string Username,
    string Password) : ICommand<AuthUserDto>;