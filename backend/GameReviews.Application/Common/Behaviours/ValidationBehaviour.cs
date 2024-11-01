using FluentValidation;
using MediatR;
using System.Reflection;
using GameReviews.Application.Common.Errors;
using GameReviews.Domain.Results;

namespace GameReviews.Application.Common.Behaviours;
internal class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var validationResults = await Task.WhenAll(
            _validators.Select(v =>
                v.ValidateAsync(request, cancellationToken)));

        var isValid = Array.TrueForAll(validationResults, r => r.IsValid);

        if (!isValid)
        {
            var errors = validationResults
                .Where(r => !r.IsValid)
                .SelectMany(r => r.Errors)
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());

            var res = CreateValidationResult<TResponse>(errors);
            return res;
        }

        return await next();
    }

    private static TResult CreateValidationResult<TResult>(IDictionary<string, string[]> errors)
        where TResult : Result
    {

        if (typeof(TResult) == typeof(Result))
        {
            return (TResult)Result.Failure(ValidationErrors.FluentValidation(errors));
        }

        var resultType = typeof(TResult).GetGenericArguments().First();

        var method = typeof(Result).GetMethods(BindingFlags.Static | BindingFlags.Public)
            .FirstOrDefault(m => m is { IsGenericMethod: true, Name: nameof(Result.Failure) });

        if (method is null)
        {
            throw new InvalidOperationException("Result class must contain generic static public Failure method");
        }

        var genericMethod = method.MakeGenericMethod(resultType);

        var validationError = ValidationErrors.FluentValidation(errors);

        var result = genericMethod.Invoke(null, new object[] { validationError });

        return (TResult)result!;
    }
}