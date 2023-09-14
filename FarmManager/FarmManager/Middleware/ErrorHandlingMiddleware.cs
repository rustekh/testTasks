using System.Net;

internal class ErrorHandlingMiddleware
{
    private RequestDelegate _next;
    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (System.Exception ex)
        {
            HandleExceptionAsync(context, ex);
        }
    }

    private void HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        if (exception is ValidationException)
        {
            code = HttpStatusCode.BadRequest;
        }

        context.Response.StatusCode = (int)code;
    }
}