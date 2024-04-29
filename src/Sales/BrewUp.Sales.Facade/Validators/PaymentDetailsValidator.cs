using BrewUp.Shared.Contracts;
using FluentValidation;

namespace BrewUp.Sales.Facade.Validators;

public class PaymentDetailsValidator : AbstractValidator<PaymentDetailsJson>
{
    public PaymentDetailsValidator()
    {
        RuleFor(v => v.CreditCardNumber).NotEmpty();
        RuleFor(v => v.CreditCardExpirationDate).NotEmpty();
        RuleFor(v => v.CreditCardSecurityCode).NotEmpty();
    }
}