﻿using FluentValidation;
using GameReviews.Domain.Common.Abstractions.Repositories;

namespace GameReviews.Application.Users.Commands.CreateUserEntity;

internal sealed class CreateUserEntityCommandValidator : AbstractValidator<CreateUserEntityCommand>
{
    public CreateUserEntityCommandValidator(IUsersRepository usersRepository)
    {
        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email address")
            .DependentRules(() =>
            {
                RuleFor(u => u.Email).MustAsync(async (email, token) =>
                {
                    return !await usersRepository.IsEmailExistsAsync(email);
                }).WithMessage("User with this email has already exist");
            });

        RuleFor(u => u.Username)
            .NotEmpty().WithMessage("Username is required")
            .MinimumLength(5)
            .MaximumLength(12)
            .MustAsync(async (username, token) =>
            {
                return !await usersRepository.IsUsernameExistsAsync(username);
            }).WithMessage("User with this username has already exist");

        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("Password is required.")
            .Length(8, 48).WithMessage("Password must be between 8 and 48 characters.")
            .Matches(@"[A-Za-z]").WithMessage("Password must contain at least one letter.")
            .Matches(@"\d").WithMessage("Password must contain at least one number.")
            .Matches(@"[\W]").WithMessage("Password must contain at least one special character.");
    }
}