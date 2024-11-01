using GameReviews.Domain.Common.Abstractions.Entities;
using GameReviews.Domain.Common.Abstractions.Repositories;
using GameReviews.Domain.Common.Rules;
using GameReviews.Domain.Entities.UserAggregate.Rules;
using GameReviews.Domain.Results;

namespace GameReviews.Domain.Entities.UserAggregate.ValueObjects;

public record Username
{
    public string Value { get; init; }

    private Username(string value)
    {
        Value = value;
    }

    internal static async Task<Result<Username>> CreateAsync(string username, IUsersRepository usersRepository)
    {
        var result = await BusinessRuleValidator.CheckRulesAsync(new UsernameMustBeUniqueRule(username, usersRepository));
        if (result.IsFailure)
        {
            return result.Error;
        }
        
        return new Username(username);
    }
}