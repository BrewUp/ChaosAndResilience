using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;

namespace BrewUp.Sales.ReadModel.Dtos;

public class PaymentDetails
{
    public string CreditCardNumber { get; private set; } = string.Empty;
    public DateTime CreditCardExpirationDate { get; private set; } = DateTime.MinValue;
    public string CreditCardSecurityCode { get; private set; } = string.Empty;
    
    protected PaymentDetails()
    {}

    public static PaymentDetails CreatePaymentDetails(CreditCardNumber creditCardNumber,
        CreditCardExpirationDate creditCardExpirationDate, CreditCardSecurityCode creditCardSecurityCode) =>
        new(creditCardNumber.Value, creditCardExpirationDate.Value, creditCardSecurityCode.Value);
    
    private PaymentDetails(string creditCardNumber, DateTime creditCardExpirationDate, string creditCardSecurityCode)
    {
        CreditCardNumber = creditCardNumber;
        CreditCardExpirationDate = creditCardExpirationDate;
        CreditCardSecurityCode = creditCardSecurityCode;
    }
    
    public PaymentDetailsJson ToJson() => new(CreditCardNumber, CreditCardExpirationDate, CreditCardSecurityCode);
}