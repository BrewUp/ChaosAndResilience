using BrewUp.Sales.Domain.Helpers;
using BrewUp.Sales.SharedKernel.CustomTypes;
using BrewUp.Sales.SharedKernel.Events;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainIds;
using Muflone.Core;

namespace BrewUp.Sales.Domain.Entities;

public class SalesOrder : AggregateRoot
{
	internal SalesOrderId _salesOrderId;
	internal SalesOrderNumber _salesOrderNumber;
	internal OrderDate _orderDate;

	internal CustomerId _customerId;
	internal CustomerName _customerName;
	
	internal PaymentDetails _paymentDetails;
	internal DeliveryAddress _deliveryAddress;

	internal IEnumerable<SalesOrderRow> _rows;

	protected SalesOrder()
	{
	}

	internal static SalesOrder CreateSalesOrder(SalesOrderId salesOrderId, Guid correlationId,
		SalesOrderNumber salesOrderNumber, OrderDate orderDate, CustomerId customerId, CustomerName customerName,
		PaymentDetailsJson paymentDetails, DeliveryAddressJson deliveryAddress, IEnumerable<SalesOrderRowJson> rows)
	{
		return new SalesOrder(salesOrderId, correlationId, salesOrderNumber, orderDate, customerId, customerName,
			paymentDetails, deliveryAddress, rows);
	}

	private SalesOrder(SalesOrderId salesOrderId, Guid correlationId, SalesOrderNumber salesOrderNumber, OrderDate orderDate,
		CustomerId customerId, CustomerName customerName, PaymentDetailsJson paymentDetails, DeliveryAddressJson deliveryAddress, 
		IEnumerable<SalesOrderRowJson> rows)
	{
		// Check SalesOrder invariants
		RaiseEvent(new SalesOrderFromPortalCreated(salesOrderId, correlationId, salesOrderNumber, orderDate, customerId,
			customerName, paymentDetails, deliveryAddress, rows));
	}

	private void Apply(SalesOrderFromPortalCreated @event)
	{
		Id = @event.SalesOrderId;

		_salesOrderId = @event.SalesOrderId;
		_salesOrderNumber = @event.SalesOrderNumber;
		_orderDate = @event.OrderDate;
		_customerId = @event.CustomerId;
		_customerName = @event.CustomerName;

		_paymentDetails = @event.PaymentDetails.MapToDomainPaymentDetails();
		_deliveryAddress = @event.DeliveryAddress.MapToDomainDeliveryAddress();
		
		_rows = @event.Rows.MapToDomainRows();
	}
}