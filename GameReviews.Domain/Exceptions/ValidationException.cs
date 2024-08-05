using System.Net;

namespace GameReviews.Domain.Exceptions;

public class ValidationException : RequestException
{
    private const HttpStatusCode ValidationExceptionStatusCode = HttpStatusCode.BadRequest;
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException(string? message, IDictionary<string, string[]> errors) : base(
        ValidationExceptionStatusCode, message)
    {
        Errors = errors;
    }

    public ValidationException(IDictionary<string, string[]> errors) : base(ValidationExceptionStatusCode,
        "One or more validation errors occured.")
    {
        Errors = errors;
    }
}

