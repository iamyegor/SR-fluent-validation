using Api.DTOs;
using Api.FluentValidation.CustomRules;
using FluentValidation;

namespace Api.FluentValidation.Validators;

public class AddressesValidator : AbstractValidator<AddressDto[]>
{
    public AddressesValidator()
    {
        RuleFor(x => x)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .InRange(1, 3)
            .ForEach(x => x.SetValidator(new AddressDtoValidator()));
    }
}
