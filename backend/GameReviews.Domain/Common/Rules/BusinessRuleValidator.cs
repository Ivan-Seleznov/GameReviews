using GameReviews.Domain.Common.Abstractions.Entities;
using GameReviews.Domain.Common.Errors;
using GameReviews.Domain.Results;

namespace GameReviews.Domain.Common.Rules;

internal static class BusinessRuleValidator
{
    internal static async Task<Result> CheckRulesAsync(params IBusinessRule[] rules)
    {
        IDictionary<string,string> errors = new Dictionary<string, string>();
        foreach (var rule in rules)
        {
            if (await rule.IsBrokenAsync())
            {
                errors.Add(rule.GetType().Name, rule.Message);
            }
        }
        return errors.Count > 0 ? DomainErrors.RuleBroken(errors) : Result.Success();
    }
}