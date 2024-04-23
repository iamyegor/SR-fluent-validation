using System.Text.RegularExpressions;
using Api.DTOs;
using FluentValidation;

namespace Api.FluentValidation;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(0, 200);
        RuleFor(x => x.Addresses).SetValidator(new AddressesValidator());

        When(x => x.Email == null, () =>
        {
            RuleFor(x => x.Phone).NotEmpty();
        });
        When(x => x.Phone == null, () =>
        {
            RuleFor(x => x.Email).NotEmpty();
        });

        RuleFor(x => x.Email)
            .NotEmpty()
            .Length(0, 150)
            .EmailAddress()
            .When(x => x.Email != null, ApplyConditionTo.CurrentValidator);

        RuleFor(x => x.Phone)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Must(x => Regex.IsMatch(x, "^[2-9][0-9]{9}$"))
            .When(x => x.Phone != null);
    }
}
