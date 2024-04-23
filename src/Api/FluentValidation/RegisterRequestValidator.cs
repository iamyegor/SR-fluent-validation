using System.Text.RegularExpressions;
using Api.DTOs;
using FluentValidation;

namespace Api.FluentValidation;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(0, 200);
        RuleFor(x => x.Email).NotEmpty().Length(0, 150).EmailAddress();
        RuleFor(x => x.Addresses).SetValidator(new AddressesValidator());
        
        RuleFor(x => x.Phone)
            .NotEmpty()
            .Must(x => Regex.IsMatch(x, "^[2-9][0-9]{9}$"))
            .When(x => x.Phone != null, ApplyConditionTo.CurrentValidator)
            .WithMessage("The phone number is incorrect");
    }
}
