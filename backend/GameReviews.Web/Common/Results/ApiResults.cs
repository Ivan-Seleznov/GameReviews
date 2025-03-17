using GameReviews.Domain.Results;
using GameReviews.Domain.Results.Errors;
namespace GameReviews.Web.Common.Results;

public static class ApiResults
{
    public static IResult CreateProblemDetails(Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException();
        }
        
        return Microsoft.AspNetCore.Http.Results.Problem(
            statusCode: GetStatusCode(result.Error.ErrorType),
            title: GetTitle(result.Error.ErrorType),
            type: GetProblemType(result.Error.ErrorType),
            extensions: GetExtensions(result.Error));

        IDictionary<string, object?>? GetExtensions(Error error)
        {
            return error switch
            {
                ValidationError validationError => new Dictionary<string, object?>
                {
                    { "errors", validationError.Errors }
                },
                _ => new Dictionary<string, object?>()
                {
                    { "error", error }
                }
            };
        }

        static string GetTitle(ErrorType type)
        {
            return type switch
            {
                ErrorType.Failure => "BadRequest",
                ErrorType.NotFound => "NotFound",
                ErrorType.Validation => "Validation",
                _ => "Server failure"
            };
        }

        static string GetProblemType(ErrorType type)
        {
            return type switch
            {
                ErrorType.Failure => "https://example.com/errors/failure",
                ErrorType.NotFound => "https://example.com/errors/not-found",
                ErrorType.Validation => "https://example.com/errors/validation",
                _ => "https://example.com/errors/unknown"
            };
        }


        static int GetStatusCode(ErrorType type)
        {
            return type switch
            {
                ErrorType.Failure => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };
        }
    }
}