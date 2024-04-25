using Api.DTOs;
using Api.FluentValidation.CustomRules;
using DomainModel;
using FluentValidation;

namespace Api.FluentValidation.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(0, 200);
        RuleFor(x => x.Addresses).SetValidator(new AddressesValidator());

        When(
            x => x.Email == null,
            () =>
            {
                RuleFor(x => x.Phone).NotEmpty();
            }
        );
        When(
            x => x.Phone == null,
            () =>
            {
                RuleFor(x => x.Email).NotEmpty();
            }
        );

        RuleFor(x => x.Email).MustBeSuccessful(x => Email.Create(x)).When(x => x.Email != null);

        RuleFor(x => x.Phone).NotEmpty().Matches("^[2-9][0-9]{9}$").When(x => x.Phone != null);
    }
}
