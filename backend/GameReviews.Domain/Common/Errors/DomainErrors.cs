using GameReviews.Domain.Results.Errors;

namespace GameReviews.Domain.Common.Errors;

internal static class DomainErrors
{
    public static Error RuleBroken(IDictionary<string,string> errors) => 
        new BusinessValidationError("DomainError.Validation.Business","One ore more business rules are broken",errors , ErrorType.Failure);
}