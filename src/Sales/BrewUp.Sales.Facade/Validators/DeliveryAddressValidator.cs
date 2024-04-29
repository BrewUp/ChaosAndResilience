using BrewUp.Shared.Contracts;
using FluentValidation;

namespace BrewUp.Sales.Facade.Validators;

public class DeliveryAddressValidator : AbstractValidator<DeliveryAddressJson>
{
    public DeliveryAddressValidator()
    {
        RuleFor(v => v.Name).NotEmpty();
        RuleFor(v => v.Street).NotEmpty();
        RuleFor(v => v.City).NotEmpty();
        RuleFor(v => v.State).NotEmpty();
        RuleFor(v => v.ZipCode).NotEmpty();
    }
}