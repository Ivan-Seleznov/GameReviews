using GameReviews.Domain.Common.Abstractions.Repositories;
using GameReviews.Domain.Common.Rules;
using GameReviews.Domain.Entities.UserAggregate.Rules;
using GameReviews.Domain.Results;
using GameReviews.Domain.Results.Errors;

namespace GameReviews.Domain.Entities.UserAggregate.ValueObjects;

public record Email
{
    public string Value { get; init; }
    
    private Email(string value)
    {
        Value = value;
    }
    
    internal static async Task<Result<Email>> CreateAsync(string email, IUsersRepository usersRepository)
    {
        var result = await BusinessRuleValidator.CheckRulesAsync(new EmailMustBeUniqueRule(email, usersRepository));
        if (result.IsFailure)
        {
            return result.Error;
        }

        return new Email(email);
    }
}