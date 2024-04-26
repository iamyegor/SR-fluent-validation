using DomainModel;
using FluentValidation;

namespace Api.FluentValidation.Extensions;

public static class ValidationContextExtensions
{
    public static void AddError<T>(this ValidationContext<T> context, Error error)
    {
        context.AddFailure(error.Serialize());
    }
}
