using System.Diagnostics;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace ExceptionHandling;

public static class ServiceCollectionExtensions
{
    private static readonly string DefaultErrorMessage = "See logs for details";

    private static Dictionary<Type, HttpStatusCode> errors = new();

    public static void AddExceptionHandling<TException>(
        this IApplicationBuilder app,
        HttpStatusCode code)
    where TException : ApplicationException
    {
        if (app is null)
            throw new ArgumentNullException(nameof(app));

        errors.Add(typeof(TException), code);
        
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()
                    ?.Error;
                if (exception is not null)
                {
                    if (errors.TryGetValue(exception.GetType(), out HttpStatusCode errorResult))
                    {
                        await ConfigureExceptionResponse(context, errorResult, exception.Message);
                        return;
                    }

                    await ConfigureExceptionResponse(context, HttpStatusCode.InternalServerError, exception.Message);
                }
            });
        });
    }
    
    private static async Task ConfigureExceptionResponse(HttpContext context, HttpStatusCode code, string message)
    {
        var errorMessage = ConfigureErrorMessage(code, message);
        
        context.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Json;
        context.Response.StatusCode = (int)code;

        var errorResult = new ErrorResult
        {
            Code = code,
            Message = errorMessage
        };
                        
        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResult));
    }

    private static string ConfigureErrorMessage(HttpStatusCode code, string message)
    {
        return code is HttpStatusCode.InternalServerError ? DefaultErrorMessage : message;
    }
}