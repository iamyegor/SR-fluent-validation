using System.Linq;
using System.Net;
using Api.Controllers.Common;
using DomainModel.DomainErrors;
using Microsoft.AspNetCore.Mvc;

namespace Api.FluentValidation.ResponseFactories;

public static class ValidationErrorResponseFactory
{
    public static IActionResult Create(ActionContext context)
    {
        string serializedError = context.ModelState.Values.First().Errors.First().ErrorMessage;
        Error error = Error.Deserialize(serializedError);

        return new ErrorResult(error, HttpStatusCode.UnprocessableContent);
    }
}
