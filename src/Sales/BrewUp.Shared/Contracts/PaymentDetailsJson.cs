namespace BrewUp.Shared.Contracts;

public record PaymentDetailsJson(string CreditCardNumber, DateTime CreditCardExpirationDate, string CreditCardSecurityCode);