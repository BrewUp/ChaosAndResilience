using BrewUp.Sales.Domain.Entities;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainIds;
using PaymentDetails = BrewUp.Sales.ReadModel.Dtos.PaymentDetails;

namespace BrewUp.Sales.Domain.Helpers;

public static class DomainHelper
{
	internal static SalesOrderRow MapToDomainRow(this SalesOrderRowJson json)
	{
		return SalesOrderRow.CreateSalesOrderRow(new BeerId(json.BeerId), new BeerName(json.BeerName), json.Quantity, json.Price);
	}

	internal static IEnumerable<SalesOrderRow> MapToDomainRows(this IEnumerable<SalesOrderRowJson> json)
	{
		return json.Select(r => SalesOrderRow.CreateSalesOrderRow(new BeerId(r.BeerId), new BeerName(r.BeerName), r.Quantity, r.Price));
	}
	
	internal static Entities.PaymentDetails MapToDomainPaymentDetails(this PaymentDetailsJson json)
	{
		return Entities.PaymentDetails.CreatePaymentDetails(new CreditCardNumber(json.CreditCardNumber),
			new CreditCardExpirationDate(json.CreditCardExpirationDate),
			new CreditCardSecurityCode(json.CreditCardSecurityCode));
	}
	
	internal static PaymentDetailsJson MapToReadModelPaymentDetails(this Entities.PaymentDetails paymentDetails)
	{
		return new PaymentDetailsJson(paymentDetails.CreditCardNumber.Value,
			paymentDetails.CreditCardExpirationDate.Value, paymentDetails.CreditCardSecurityCode.Value);
	}
	
	internal static DeliveryAddress MapToDomainDeliveryAddress(this DeliveryAddressJson json)
	{
		return DeliveryAddress.CreateDeliveryAddress(new AddressName(json.Name), new Street(json.Street), new City(json.City),
			new State(json.State), new ZipCode(json.ZipCode), new PhoneNumber(json.PhoneNumber));
	}
	
	internal static DeliveryAddressJson MapToReadModelDeliveryAddress(this Entities.DeliveryAddress deliveryAddress)
	{
		return new DeliveryAddressJson(deliveryAddress.AddressName.Value, deliveryAddress.Street.Value, deliveryAddress.City.Value,
			deliveryAddress.State.Value, deliveryAddress.ZipCode.Value, deliveryAddress.PhoneNumber.Value);
	}
	
	internal static ReadModel.Dtos.SalesOrder MapToReadModel(this SalesOrder salesOrder)
	{
		return ReadModel.Dtos.SalesOrder.CreateSalesOrder(salesOrder._salesOrderId, salesOrder._salesOrderNumber,
						salesOrder._customerId, salesOrder._customerName, salesOrder._orderDate,
						salesOrder._paymentDetails.MapToReadModelPaymentDetails(),
						salesOrder._deliveryAddress.MapToReadModelDeliveryAddress(),
									salesOrder._rows.Select(r => new SalesOrderRowJson
									{
										BeerId = r._beerId.Value,
										BeerName = r._beerName.Value,
										Quantity = r._quantity,
										Price = r._beerPrice
									}));
	}
}