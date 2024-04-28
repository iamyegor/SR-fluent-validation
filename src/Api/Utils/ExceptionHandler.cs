using System;
using System.Net;
using System.Threading.Tasks;
using DomainModel.DomainErrors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Api.Utils;

public sealed class ExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _env;

    public ExceptionHandler(RequestDelegate next, IWebHostEnvironment env)
    {
        _next = next;
        _env = env;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }

    private Task HandleException(HttpContext context, Exception exception)
    {
        string errorMessage = _env.IsProduction()
            ? "Internal server error"
            : "Exception: " + exception.Message;

        Error error = Errors.Server.InternalServerError(errorMessage);
        string result = JsonConvert.SerializeObject(
            new { errorCode = error.ErrorCode, errorMessage = error.ErrorMessage }
        );

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        return context.Response.WriteAsync(result);
    }
}
