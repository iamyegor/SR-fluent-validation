using Api.DTOs;
using FluentValidation;

namespace Api.FluentValidation;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().Length(0, 150).EmailAddress();
    }
}
