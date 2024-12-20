﻿using GameReviews.Domain.Results.Errors;

namespace GameReviews.Application.Common.Errors;
public static class ValidationErrors
{
    public static ValidationError FluentValidation(IDictionary<string, string[]> errors) =>
        new("Error.Validation", "One or more validation errors occurred", errors, ErrorType.Validation);
}