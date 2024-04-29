using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.Entities;

namespace BrewUp.Sales.Domain.Entities;

public class PaymentDetails : EntityBase
{
    internal CreditCardNumber CreditCardNumber;
    internal CreditCardExpirationDate CreditCardExpirationDate;
    internal CreditCardSecurityCode CreditCardSecurityCode;
    
    protected PaymentDetails()
    {}
    
    internal static PaymentDetails CreatePaymentDetails(CreditCardNumber creditCardNumber, CreditCardExpirationDate creditCardExpirationDate,
        CreditCardSecurityCode creditCardSecurityCode)
    {
        return new PaymentDetails(creditCardNumber, creditCardExpirationDate, creditCardSecurityCode);
    }
    
    private PaymentDetails(CreditCardNumber creditCardNumber, CreditCardExpirationDate creditCardExpirationDate,
        CreditCardSecurityCode creditCardSecurityCode)
    {
        CreditCardNumber = creditCardNumber;
        CreditCardExpirationDate = creditCardExpirationDate;
        CreditCardSecurityCode = creditCardSecurityCode;
    }
}