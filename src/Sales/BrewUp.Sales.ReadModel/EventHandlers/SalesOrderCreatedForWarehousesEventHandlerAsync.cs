using BrewUp.Sales.SharedKernel.Events;
using Microsoft.Extensions.Logging;
using Muflone;

namespace BrewUp.Sales.ReadModel.EventHandlers;

public sealed class SalesOrderCreatedForWarehousesEventHandlerAsync(ILoggerFactory loggerFactory,
		IEventBus eventBus)
	: DomainEventHandlerBase<SalesOrderFromPortalCreated>(loggerFactory)
{
	public override async Task HandleAsync(SalesOrderFromPortalCreated @event, CancellationToken cancellationToken = new())
	{
		cancellationToken.ThrowIfCancellationRequested();
		
		try
		{
			var correlationId =
				new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);
			
			SalesOrderConfirmed integrationEvent =
				new(@event.SalesOrderId, correlationId, @event.Rows);
			await eventBus.PublishAsync(integrationEvent, cancellationToken);
		}
		catch (Exception ex)
		{
			Logger.LogError(ex, "Error handling sales order created event to update warehouses");
			throw;
		}
	}
}