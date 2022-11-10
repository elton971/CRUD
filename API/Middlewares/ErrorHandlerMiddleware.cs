using System.Net;
using Aplication.Errors;
using Newtonsoft.Json;

namespace API.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch ( Exception e)
        {
            await handleExceptionAsync(context, e, _logger);
        }
    }

    private async Task handleExceptionAsync(HttpContext context, Exception exception, ILogger<ErrorHandlerMiddleware> logger)
    {
        object errors = null;
        switch (exception)
        {
            case RestException re:
            {
                logger.LogError(exception,"REST ERROR");
                errors = re.Errors;
                context.Response.StatusCode = (int)re.Code;
            }
                break;
            case Exception e:
            {
                logger.LogError(exception,"Server ERROR");
                errors = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
                break;
        }
        context.Response.ContentType="application/json";
        if (errors != null)
        {
            var result = JsonConvert.SerializeObject(new
            {
                errors
            });
            await context.Response.WriteAsync(result);
        }
    }
}
