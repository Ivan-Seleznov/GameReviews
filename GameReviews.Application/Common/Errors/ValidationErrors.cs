using GameReviews.Domain.Common.Result.Errors;

namespace GameReviews.Application.Common.Errors;
public static class ValidationErrors
{
    public static ValidationError FluentValidation(IDictionary<string, string[]> errors) =>
        new("ValidationError", "One or more validation errors occurred", errors, ErrorType.Validation);
}