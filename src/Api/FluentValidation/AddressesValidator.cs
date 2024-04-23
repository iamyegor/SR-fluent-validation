using Api.DTOs;
using FluentValidation;

namespace Api.FluentValidation;

public class AddressesValidator : AbstractValidator<AddressDto[]>
{
    public AddressesValidator()
    {
        RuleFor(x => x)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .Must(x => x?.Length is >= 1 and <= 3)
            .WithMessage("The number of addresses must be between 1 and 3")
            .ForEach(x => x.SetValidator(new AddressDtoValidator()));
    }
}
