using System.Diagnostics.CodeAnalysis;
using GameReviews.Domain.Results.Errors;

namespace GameReviews.Domain.Results;
public class Result
{
    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None || !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }
        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    public Error Error { get; }
    public static Result Success() => new(true, Error.None);
    public static Result<T> Success<T>(T value) => new(true, Error.None, value);

    public static Result Failure(Error error) => new(false, error);
    public static Result<T> Failure<T>(Error error) => new(false, error, default!);
    public static explicit operator bool(Result result) => result.IsSuccess;
}

public class Result<T> : Result
{
    private readonly T? _value;
    public Result(bool isSuccess, Error error, T? value) : base(isSuccess, error)
    {
        _value = value;
    }

    [NotNull]
    public T Value =>
        IsSuccess ? _value! : throw new InvalidOperationException("Cannot access the Value property when the Result indicates a failure");

    public T ValueOrDefault => IsSuccess ? _value! : default!;

    public static implicit operator Result<T>(T? value) =>
        value is not null ? Success(value) : Failure<T>(Error.NullValue);

    public static implicit operator Result<T>(Error error) => Failure<T>(error);
}