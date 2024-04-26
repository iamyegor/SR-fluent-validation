using Api.DTOs;
using Api.FluentValidation.CustomRules;
using Api.Repositories;
using DomainModel;
using FluentValidation;

namespace Api.FluentValidation.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator(StatesRepository statesRepository)
    {
        RuleFor(x => x.Name).MustNotBeEmpty().Length(0, 200);
        RuleFor(x => x.Addresses).SetValidator(new AddressesValidator(statesRepository));

        When(
            x => x.Email == null,
            () =>
            {
                RuleFor(x => x.Phone).MustNotBeEmpty();
            }
        );
        When(
            x => x.Phone == null,
            () =>
            {
                RuleFor(x => x.Email).MustNotBeEmpty();
            }
        );

        RuleFor(x => x.Email).MustBeSuccessful(Email.Create).When(x => x.Email != null);

        RuleFor(x => x.Phone)
            .MustNotBeEmpty()
            .Matches("^[2-9][0-9]{9}$")
            .When(x => x.Phone != null);
    }
}
