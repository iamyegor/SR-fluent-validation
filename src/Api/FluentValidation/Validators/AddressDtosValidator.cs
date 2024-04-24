using Api.DTOs;
using Api.FluentValidation.CustomRules;
using FluentValidation;

namespace Api.FluentValidation.Validators;

public class AddressDtosValidator : AbstractValidator<AddressDto[]>
{
    public AddressDtosValidator()
    {
        RuleFor(x => x)
            .InRange(1, 3)
            .ForEach(x =>
            {
                x.NotNull().SetValidator(new AddressDtoValidator());
            });
    }
}
