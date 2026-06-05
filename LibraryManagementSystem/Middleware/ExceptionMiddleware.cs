using System.Net;
using System.Text.Json;
using LibraryManagementSystem.Common.Models;
using LibraryManagementSystem.Store.Abstractions;
using Serilog;

namespace LibraryManagementSystem.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(
        RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext context,
        IErrorLogStore errorLogStore)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            Log.Error(
                ex,
                ex.Message);

            try
            {
                var errorLog =
                    new ErrorLog
                    {
                        UniqueId =
                            Guid.NewGuid()
                                .ToString("N")
                                .ToUpper(),

                        ErrorLogGuid =
                            Guid.NewGuid(),

                        ErrorSource =
                            ex.TargetSite?.Name,

                        ErrorMessage =
                            ex.Message,

                        ErrorProcedure =
                            ex.TargetSite?.Name,

                        ErrorStackTrace =
                            ex.StackTrace,

                        RequestPath =
                            context.Request.Path,

                        UserName =
                            context.User?
                                .Identity?
                                .Name,

                        LogLevel =
                            "Error",

                        LoggedOn =
                            DateTime.UtcNow,

                        IsActive =
                            true,

                        CreatedOn =
                            DateTime.UtcNow
                    };

                await errorLogStore
                    .CreateAsync(
                        errorLog);
            }
            catch
            {
                // prevent logging failures
                // from crashing API
            }

            context.Response.Clear();

            context.Response.StatusCode =
                (int)
                HttpStatusCode
                    .InternalServerError;

            context.Response.ContentType =
                "application/json";

            var result =
                JsonSerializer.Serialize(
                    new
                    {
                        Success = false,
                        Message = ex.Message
                    });

            await context.Response
                .WriteAsync(result);
        }
    }
}