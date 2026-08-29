using System.Net;
using CleanArchTemplate.Application.Common.Exceptions;
using CleanArchTemplate.Infrastructure.Http;

namespace CleanArchTemplate.Api.Middlewares;

/// <summary>
/// Tratamento global de exceptions. Evita try/catch repetido em controllers/endpoints.
/// Inclua aqui novas exceptions de aplicacao e o status HTTP correspondente.
/// </summary>
public sealed class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unhandled exception.");
            await WriteProblemDetailsAsync(context, exception);
        }
    }

    private static async Task WriteProblemDetailsAsync(HttpContext context, Exception exception)
    {
        var (statusCode, title, errors) = exception switch
        {
            ValidationException validation => ((int)HttpStatusCode.BadRequest, "Validation error", validation.Errors),
            NotFoundException => ((int)HttpStatusCode.NotFound, "Resource not found", new[] { exception.Message }),
            ExternalHttpRequestException external => ((int)external.StatusCode, "External service error", new[] { external.Message }),
            ArgumentException => ((int)HttpStatusCode.BadRequest, "Invalid request", new[] { exception.Message }),
            _ => ((int)HttpStatusCode.InternalServerError, "Internal server error", new[] { "An unexpected error occurred." })
        };

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(new
        {
            title,
            status = statusCode,
            errors
        });
    }
}
