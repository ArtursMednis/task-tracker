using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using TaskTracker.Application.Exceptions;

namespace TaskTracker.Api.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(
        HttpContext context,
        Exception ex)
        {
            logger.LogError(ex, ex.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;


            var options = new JsonSerializerOptions
            { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var json = JsonSerializer.Serialize(FormatProblem(ex), options);

            await context.Response.WriteAsync(json);
        }

        private ProblemDetails FormatProblem(Exception ex)
        {
            if (env.IsDevelopment())
            {
                return new ProblemDetails
                {
                    Status = 500,
                    Detail = ex.ToString(),
                    Title = ex.Message
                };
            }
            switch (ex)
            {
                case UnauthorizedAccessException unauthorizedException:
                    return new ProblemDetails
                    {
                        Title = "Unauthorized",
                        Status = (int)HttpStatusCode.Unauthorized,
                        Detail = "Unauthorized"
                    };
                case TaskNotFoundException notFound:
                    return new ProblemDetails
                    {
                        Title = "Not Found",
                        Status = (int)HttpStatusCode.NotFound,
                        Detail = "Not Found"
                    };
                default:
                    return new ProblemDetails
                    {
                        Title = "Internal Server Error",
                        Status = (int)HttpStatusCode.InternalServerError,
                        Detail = "Internal Server Error"
                    };
            }
        }

    }
}
