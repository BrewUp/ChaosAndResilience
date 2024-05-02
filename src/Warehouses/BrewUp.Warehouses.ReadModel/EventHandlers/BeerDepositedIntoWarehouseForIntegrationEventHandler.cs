using BrewUp.Warehouses.SharedKernel.Events;
using Microsoft.Extensions.Logging;
using Muflone;

namespace BrewUp.Warehouses.ReadModel.EventHandlers;

public class BeerDepositedIntoWarehouseForIntegrationEventHandler(ILoggerFactory loggerFactory,
	IEventBus eventBus) : DomainEventHandlerBase<BeerDepositedIntoWarehouse>(loggerFactory)
{
	public override async Task HandleAsync(BeerDepositedIntoWarehouse @event,
		CancellationToken cancellationToken = new())
	{
		cancellationToken.ThrowIfCancellationRequested();

		var correlationId =
			new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);

		AvailabilityUpdatedForNotification availabilityUpdatedForNotification = new(@event.BeerId, correlationId, @event.BeerName, @event.Quantity);
		await eventBus.PublishAsync(availabilityUpdatedForNotification, cancellationToken);
	}
}