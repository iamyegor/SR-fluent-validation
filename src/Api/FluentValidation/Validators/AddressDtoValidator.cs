using Api.DTOs;
using FluentValidation;

namespace Api.FluentValidation.Validators;

public class AddressDtoValidator : AbstractValidator<AddressDto>
{
    public AddressDtoValidator()
    {
        RuleFor(x => x).NotNull();
        RuleFor(x => x.Street).NotEmpty().Length(0, 100);
        RuleFor(x => x.City).NotEmpty().Length(0, 40);
        RuleFor(x => x.State).NotEmpty().Length(0, 2);
        RuleFor(x => x.ZipCode).NotEmpty().Length(0, 5);
    }
}
