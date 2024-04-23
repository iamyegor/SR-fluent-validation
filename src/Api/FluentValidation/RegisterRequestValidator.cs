using Api.DTOs;
using FluentValidation;

namespace Api.FluentValidation;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(0, 200);
        RuleFor(x => x.Email).NotEmpty().Length(0, 150).EmailAddress();

        RuleFor(x => x.Address).SetValidator(new AddressDtoValidator());
    }
}
