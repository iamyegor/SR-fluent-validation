using System;
using System.Collections.Generic;
using DomainModel.DomainErrors;
using FluentValidation;
using XResults;

namespace Api.FluentValidation.CustomRules;

public static class CustomValidationRules
{
    public static IRuleBuilderOptionsConditions<T, TProperty> MustBeSuccessful<
        T,
        TProperty,
        TValue
    >(this IRuleBuilder<T, TProperty> ruleBuilder, Func<TProperty, Result<TValue, Error>> callback)
    {
        return ruleBuilder.Custom(
            (value, context) =>
            {
                Result<TValue, Error> result = callback(value);
                if (result.IsFailure)
                {
                    context.AddFailure(result.Error.Serialize());
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
