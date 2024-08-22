using FluentValidation;
using GameReviews.Application.Common.Interfaces.Repositories;

namespace GameReviews.Application.Users.Commands.LoginUser;
public sealed class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator(IUsersRepository usersRepository)
    {
        RuleFor(u => u.Username).MustAsync(async (userName, token) =>
        {
            return await usersRepository.IsUsernameExistsAsync(userName);
        }).WithMessage("Invalid login credentials");

        //add validation for password?
    }
}