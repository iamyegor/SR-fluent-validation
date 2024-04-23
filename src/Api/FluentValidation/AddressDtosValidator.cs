using Api.DTOs;
using FluentValidation;

namespace Api.FluentValidation;

public class AddressDtosValidator : AbstractValidator<AddressDto[]>
{
    public AddressDtosValidator()
    {
        RuleFor(x => x)
            .Must(x => x?.Length is >= 1 and <= 3)
            .WithMessage("The number of addresses must be between 1 and 3")
            .ForEach(x =>
            {
                x.NotNull().SetValidator(new AddressDtoValidator());
            });
    } 
}