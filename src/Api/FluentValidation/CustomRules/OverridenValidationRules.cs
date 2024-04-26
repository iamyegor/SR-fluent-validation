using Api.FluentValidation.Extensions;
using DomainModel;
using FluentValidation;

namespace Api.FluentValidation.CustomRules;

public static class OverridenValidationRules
{
    public static IRuleBuilderOptionsConditions<T, TProperty> MustNotBeEmpty<T, TProperty>(
        this IRuleBuilder<T, TProperty> ruleBuilder
    )
    {
        return ruleBuilder.Custom(
            (value, context) =>
            {
                if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                {
                    context.AddError(Errors.Generic.IsRequired(context.PropertyPath));
                }
            }
        );
    }
}
