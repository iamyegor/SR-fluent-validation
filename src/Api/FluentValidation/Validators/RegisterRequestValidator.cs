using Api.DTOs;
using Api.FluentValidation.CustomRules;
using Api.Repositories;
using DomainModel;
using DomainModel.DomainErrors;
using FluentValidation;

namespace Api.FluentValidation.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator(StatesRepository statesRepository)
    {
        RuleFor(x => x.Name)
            .MustNotBeEmpty(_ => Errors.State.IsRequired())
            .InRange(0, 200, Errors.State.IsTooLong);
        RuleFor(x => x.Addresses).SetValidator(new AddressesValidator(statesRepository));

        When(
            x => x.Email == null,
            () =>
            {
                RuleFor(x => x.Phone).MustNotBeEmpty(_ => Errors.Phone.IsRequired());
            }
        );
        When(
            x => x.Phone == null,
            () =>
            {
                RuleFor(x => x.Email).MustNotBeEmpty(_ => Errors.Email.IsRequired());
            }
        );

        RuleFor(x => x.Email).MustBeSuccessful(Email.Create).When(x => x.Email != null);

        RuleFor(x => x.Phone)
            .MustNotBeEmpty(_ => Errors.Phone.IsRequired())
            .Matches("^[2-9][0-9]{9}$")
            .When(x => x.Phone != null);
    }
}
