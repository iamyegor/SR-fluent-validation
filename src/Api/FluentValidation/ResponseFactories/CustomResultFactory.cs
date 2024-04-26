using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;

namespace Api.FluentValidation.ResponseFactories;

public class CustomResultFactory : IFluentValidationAutoValidationResultFactory
{
    public IActionResult CreateActionResult(
        ActionExecutingContext context,
        ValidationProblemDetails validationProblemDetails
    )
    {
        if (validationProblemDetails == null)
        {
            return new OkResult();
        }

        IOptions<ApiBehaviorOptions> options =
            context.HttpContext.RequestServices.GetRequiredService<IOptions<ApiBehaviorOptions>>();

        return options.Value.InvalidModelStateResponseFactory(context);
    }
}
