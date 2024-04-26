using System;
using Api.FluentValidation.Extensions;
using DomainModel.DomainErrors;
using FluentValidation;

namespace Api.FluentValidation.CustomRules;

public static class OverridenValidationRules
{
    public static IRuleBuilderOptionsConditions<T, TProperty> MustNotBeEmpty<T, TProperty>(
        this IRuleBuilder<T, TProperty> ruleBuilder,
        Func<TProperty, Error> func
    )
    {
        return ruleBuilder.Custom(
            (value, context) =>
            {
                if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                {
                    Error error = func(value);
                    context.AddError(error);
                }
            }
        );
    }
}
