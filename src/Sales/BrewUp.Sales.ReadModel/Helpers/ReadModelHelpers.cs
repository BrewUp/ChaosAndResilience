using BrewUp.Sales.ReadModel.Dtos;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;

namespace BrewUp.Sales.ReadModel.Helpers;

public static class ReadModelHelpers
{
	public static IEnumerable<SalesOrderRow> ToReadModelEntities(this IEnumerable<SalesOrderRowJson> dtos)
	{
		return dtos.Select(dto =>
			new SalesOrderRow
			{
				BeerId = dto.BeerId.ToString(),
				BeerName = dto.BeerName,
				Quantity = dto.Quantity,
				Price = dto.Price
			});
	}

	public static PaymentDetails ToPaymentReadModel(this PaymentDetailsJson json)
	{
		return PaymentDetails.CreatePaymentDetails(new CreditCardNumber(json.CreditCardNumber),
			new CreditCardExpirationDate(json.CreditCardExpirationDate),
			new CreditCardSecurityCode(json.CreditCardSecurityCode));
	}

	public static DeliveryAddress ToDeliveryAdddressReadModel(this DeliveryAddressJson json)
	{
		return DeliveryAddress.CreateDeliveryAddress( new AddressName(json.Name), new Street(json.Street), new City(json.City),
			new State(json.State), new ZipCode(json.ZipCode), new PhoneNumber(json.PhoneNumber));
	}
}