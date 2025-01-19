using System.Net;
using Newtonsoft.Json;

namespace TestMySql.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (GlobalException ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, GlobalException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception.Type switch
            {
                GlobalException.ErrorType.BadRequest => (int)HttpStatusCode.BadRequest,
                GlobalException.ErrorType.NotFound => (int)HttpStatusCode.NotFound,
                GlobalException.ErrorType.InternalServerError => (int)HttpStatusCode.InternalServerError,
                _ => (int)HttpStatusCode.InternalServerError,
            };

            var result = JsonConvert.SerializeObject(new { error = exception.Message });
            return context.Response.WriteAsync(result);
        }
    }
}
