using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions.Handler
{
    public class CustomExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<CustomExceptionHandler> _logger;

        public CustomExceptionHandler(ILogger<CustomExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError("Error Message: {exceptionMessage}, Time of occurrence {time}", exception.Message, DateTime.UtcNow);

            (string Detail, string Title, int StatusCode) details = exception switch
            {
                InternalServerException => (
                    exception.Message,
                    exception.GetType().Name,
                    StatusCodes.Status500InternalServerError
                ),
                FluentValidation.ValidationException => (
                    exception.Message,
                    exception.GetType().Name,
                    StatusCodes.Status400BadRequest
                ),
                BadRequestException => (
                    exception.Message,
                    exception.GetType().Name,
                    StatusCodes.Status400BadRequest
                ),
                NotFoundException => (
                    exception.Message,
                    exception.GetType().Name,
                    StatusCodes.Status404NotFound
                ),
                _ => (
                    exception.Message,
                    exception.GetType().Name,
                    StatusCodes.Status500InternalServerError
                )
            };

            context.Response.StatusCode = details.StatusCode;

            var problemDetails = new ProblemDetails
            {
                Title = details.Title,
                Detail = details.Detail,
                Status = details.StatusCode,
                Instance = context.Request.Path
            };

            problemDetails.Extensions.Add("traceId", context.TraceIdentifier);

            if (exception is FluentValidation.ValidationException fvException)
            {
                var errors = fvException.Errors
                    .Select(e => new { e.PropertyName, e.ErrorMessage })
                    .ToList();
                problemDetails.Extensions.Add("ValidationErrors", errors);
            }

            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);

            return true;
        }
    }
}
