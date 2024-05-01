using BrewUp.Sales.ReadModel.Services;
using BrewUp.Sales.SharedKernel.Events;
using Microsoft.Extensions.Logging;

namespace BrewUp.Sales.ReadModel.EventHandlers;

public sealed class SalesOrderCreatedEventHandlerAsync(ILoggerFactory loggerFactory,
		ISalesOrderService salesOrderService)
	: DomainEventHandlerBase<SalesOrderFromPortalCreated>(loggerFactory)
{
	public override async Task HandleAsync(SalesOrderFromPortalCreated @event, CancellationToken cancellationToken = new())
	{
		cancellationToken.ThrowIfCancellationRequested();
		
		try
		{
			await salesOrderService.CreateSalesOrderAsync(@event.SalesOrderId, @event.SalesOrderNumber,
				@event.CustomerId, @event.CustomerName, @event.OrderDate, @event.PaymentDetails, @event.DeliveryAddress,
				@event.Rows, cancellationToken);
		}
		catch (Exception ex)
		{
			Logger.LogError(ex, "Error handling sales order created event");
			throw;
		}
	}
}