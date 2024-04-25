using Api.DTOs;
using Api.FluentValidation.CustomRules;
using Api.Repositories;
using DomainModel;
using FluentValidation;

namespace Api.FluentValidation.Validators;

public class AddressesValidator : AbstractValidator<AddressDto[]>
{
    public AddressesValidator(StatesRepository statesRepository)
    {
        RuleFor(x => x)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .InRange(1, 3)
            .ForEach(x =>
            {
                x.NotNull();
                x.MustBeSuccessful(a => State.Create(a.State, statesRepository.GetAll()));
                x.MustBeSuccessful(a =>
                {
                    State state = State.Create(a.State, statesRepository.GetAll());
                    return Address.Create(a.Street, a.City, state, a.ZipCode);
                });
            });
    }
}
