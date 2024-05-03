using BrewUp.Warehouses.ReadModel.Services;
using BrewUp.Warehouses.SharedKernel.Events;
using Microsoft.Extensions.Logging;

namespace BrewUp.Warehouses.ReadModel.EventHandlers;

public sealed class BeerRefilledIntoWarehouseEventHandler(ILoggerFactory loggerFactory,
    IAvailabilityService availabilityService)
    : DomainEventHandlerBase<BeerRefilledIntoWarehouse>(loggerFactory)
{
    public override async Task HandleAsync(BeerRefilledIntoWarehouse @event,
        CancellationToken cancellationToken = new())
    {
        cancellationToken.ThrowIfCancellationRequested();

        await availabilityService.UpdateAvailabilityAsync(@event.BeerId, @event.BeerName, @event.Quantity,
            cancellationToken);
    }
}