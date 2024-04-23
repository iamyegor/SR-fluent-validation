using Api.DTOs;
using FluentValidation;

namespace Api.FluentValidation;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().Length(0, 200);
        RuleFor(x => x.Email).NotEmpty().Length(0, 150).EmailAddress();
        
        RuleFor(x => x.Address).NotNull();
        RuleFor(x => x.Address.Street).NotEmpty().Length(0, 100).When(x => x.Address != null);
        RuleFor(x => x.Address.City).NotEmpty().Length(0, 40).When(x => x.Address != null);
        RuleFor(x => x.Address.State).NotEmpty().Length(0, 2).When(x => x.Address != null);
        RuleFor(x => x.Address.ZipCode).NotEmpty().Length(0, 5).When(x => x.Address != null);
    }
}
