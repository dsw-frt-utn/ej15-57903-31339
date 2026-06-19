using System.Net;
using System.Text.Json;
using Dsw2026Ej15.Domain.Exceptions;

namespace Dsw2026Ej15.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var (status, message) = ex switch
        {
            ValidationException => (HttpStatusCode.BadRequest, ex.Message),
            NotFoundException => (HttpStatusCode.NotFound, ex.Message),
            _ => (HttpStatusCode.InternalServerError, "Ocurrió un error interno")
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;
        await context.Response.WriteAsync(JsonSerializer.Serialize(new { error = message }));
    }
}
