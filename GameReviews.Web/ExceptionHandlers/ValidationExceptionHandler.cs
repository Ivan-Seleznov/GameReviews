using GameReviews.Domain.Exceptions;

namespace GameReviews.Web.ExceptionHandlers;

public class ValidationExceptionHandler : BaseExceptionHandler
{
    private readonly ILogger<ValidationExceptionHandler> _logger;

    public ValidationExceptionHandler(ILogger<ValidationExceptionHandler> logger)
    {
        _logger = logger;
    }

    public override async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is ValidationException validationException)
        {
            _logger.LogError(
                exception,
                "Validation error occured: {Message} {@Errors} {@Exception}",
                exception.Message,
                validationException.Errors,
                validationException);

            await HandleExceptionAsync(httpContext, exception, validationException.Errors);

            return true;
        }

        return false;
    }
}

