using System;
using System.Collections.Generic;
using FluentValidation;
using Results;

namespace Api.FluentValidation.CustomRules;

public static class CustomValidationRules
{
    public static IRuleBuilderOptionsConditions<T, TProperty> MustBeSuccessful<T, TProperty>(
        this IRuleBuilder<T, TProperty> ruleBuilder,
        Func<TProperty, Result> callback
    )
    {
        return ruleBuilder.Custom(
            (value, context) =>
            {
                Result result = callback(value);
                if (result.IsFailure)
                {
                    context.AddFailure(result.ErrorMessage);
                }
            }
        );
    }

    public static IRuleBuilderOptionsConditions<T, IList<TElement>> InRange<T, TElement>(
        this IRuleBuilder<T, IList<TElement>> ruleBuilder,
        int min,
        int max
    )
    {
        return ruleBuilder.Custom(
            (list, context) =>
            {
                if (list.Count < min || list.Count > max)
                {
                    context.AddFailure(
                        $"The list must contain more than {min} items and less than {max}. Currently, It contains {list.Count} items."
                    );
                }
            }
        );
    }
}
