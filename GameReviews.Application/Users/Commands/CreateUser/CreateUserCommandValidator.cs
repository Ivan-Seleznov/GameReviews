using FluentValidation;
using GameReviews.Application.Users.Repository;

namespace GameReviews.Application.Users.Commands.CreateUser;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator(IUsersRepository usersRepository)
    {
        RuleFor(u => u.Email).MustAsync(async (email, token) =>
        {
            return await usersRepository.IsEmailUniqueAsync(email);
        }).WithMessage("The email must be unique");

        RuleFor(u => u.Username).MustAsync(async (userName, token) =>
        {
            return await usersRepository.IsUsernameUniqueAsync(userName);
        }).WithMessage("The username must be unique");
    }
}

