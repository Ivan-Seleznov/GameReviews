namespace GameReviews.Domain.Common.Result.Errors;
public record Error
{
    public static readonly Error None = new (string.Empty, string.Empty, ErrorType.Failure);
    public static readonly Error NullValue = new("The result value is null", string.Empty, ErrorType.Failure);

    public Error(string code, string message, ErrorType errorType)
    {
        Code = code;
        Message = message;
        ErrorType = errorType;
    }

    public string Code { get; }
    public string Message { get; }
    public ErrorType ErrorType { get; }

    public static implicit operator Result(Error error) => Result.Failure(error);

    public static Error Failure(string code, string message) => new(code, message, ErrorType.Failure);
    public static Error Validation(string code, string message) => new(code, message, ErrorType.Validation);
    public static Error NotFound(string code, string message) => new(code, message, ErrorType.NotFound);
}