namespace GameReviews.Domain.Results.Errors;

public static class DomainErrors
{
    public static Error RuleBroken(string message) => new Error("RuleBroken", message, ErrorType.Failure);
}