using FluentValidation;
using GameReviews.Application.Common.Interfaces.Repositories;

namespace GameReviews.Application.Users.Commands.LoginUser;
public sealed class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator(IUsersRepository usersRepository)
    {
    }
}