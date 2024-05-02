namespace ResilienceBlazor.Modules.Sales.Extensions.Dtos;

public record PaymentDetailsJson(string CreditCardNumber, DateTime CreditCardExpirationDate, string CreditCardSecurityCode);