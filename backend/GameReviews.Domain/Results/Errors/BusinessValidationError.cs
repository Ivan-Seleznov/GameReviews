namespace GameReviews.Domain.Results.Errors;

public record BusinessValidationError : Error
{
    public BusinessValidationError(string code, string message, IDictionary<string, string> errors, ErrorType errorType) : base(code, message, errorType)
    {
        Errors = errors;
    }
    public IDictionary<string,string> Errors { get; }
}