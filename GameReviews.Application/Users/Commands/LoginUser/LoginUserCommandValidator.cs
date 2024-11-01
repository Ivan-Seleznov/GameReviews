using FluentValidation;
using GameReviews.Domain.Common.Abstractions.Repositories;

namespace GameReviews.Application.Users.Commands.LoginUser;
public sealed class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator(IUsersRepository usersRepository)
    {
    }
}