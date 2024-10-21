namespace GameReviews.Domain.Results.Errors;
public record ValidationError : Error
{
    public ValidationError(string code, string message, IDictionary<string, string[]> errors, ErrorType errorType) : base(code, message, errorType)
    {
        Errors = errors;
    }

    public IDictionary<string, string[]> Errors { get; }
}