#nullable enable
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DomainModel.DomainErrors;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Common;

public class ErrorResult : IActionResult
{
    private readonly Error _error;
    private readonly HttpStatusCode _statusCode;

    public ErrorResult(Error error, HttpStatusCode statusCode)
    {
        _error = error;
        _statusCode = statusCode;
    }

    public Task ExecuteResultAsync(ActionContext context)
    {
        var dictionary = new Dictionary<string, object?>()
        {
            ["code"] = _error.ErrorCode,
            ["message"] = _error.ErrorMessage
        };

        foreach (var detail in _error.Details)
        {
            dictionary.Add(detail.Key, detail.Value);
        }

        var objectResult = new ObjectResult(dictionary)
        {
            ContentTypes = ["application/vnd.yegor.problem+json"],
            StatusCode = (int)_statusCode
        };

        return objectResult.ExecuteResultAsync(context);
    }
}
