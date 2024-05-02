using BrewUp.Warehouses.ReadModel.Services;
using BrewUp.Warehouses.SharedKernel.Events;
using Microsoft.Extensions.Logging;

namespace BrewUp.Warehouses.ReadModel.EventHandlers;

public sealed class BeerDepositedIntoWarehouseEventHandler(ILoggerFactory loggerFactory,
		IAvailabilityService availabilityService)
	: DomainEventHandlerBase<BeerDepositedIntoWarehouse>(loggerFactory)
{
	public override async Task HandleAsync(BeerDepositedIntoWarehouse @event,
		CancellationToken cancellationToken = new())
	{
		cancellationToken.ThrowIfCancellationRequested();

		await availabilityService.UpdateAvailabilityAsync(@event.BeerId, @event.BeerName, @event.Quantity, cancellationToken);
	}
}