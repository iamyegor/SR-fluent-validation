using System;
using System.Collections.Generic;
using Api.FluentValidation.Extensions;
using Api.Repositories;
using DomainModel;
using DomainModel.DomainErrors;
using FluentValidation;
using XResults;
using Errors = DomainModel.DomainErrors.Errors;

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
                    context.AddError(result.Error);
                }
            }
        );
    }

    public static IRuleBuilderOptionsConditions<T, IList<TElement>> InRange<T, TElement>(
        this IRuleBuilder<T, IList<TElement>> ruleBuilder,
        int min,
        int max,
        Func<IList<TElement>, Error> func
    )
    {
        return ruleBuilder.Custom(
            (list, context) =>
            {
                if (list.Count < min || list.Count > max)
                {
                    Error error = func(list);
                    context.AddError(error);
                }
            }
        );
    }

    public static IRuleBuilderOptionsConditions<T, string> InRange<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        int min,
        int max,
        Func<string, Error> func
    )
    {
        return ruleBuilder.Custom(
            (value, context) =>
            {
                if (value.Length < min || value.Length > max)
                {
                    Error error = func(value);
                    context.AddError(error);
                }
            }
        );
    }

    public static IRuleBuilderOptionsConditions<T, string> EmailMustNotBeTaken<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        StudentRepository studentRepository
    )
    {
        return ruleBuilder.Custom(
            (value, context) =>
            {
                Email email = Email.Create(value);
                if (studentRepository.IsEmailTaken(email))
                {
                    context.AddError(Errors.Email.IsTaken(value));
                }
            }
        );
    }
}
