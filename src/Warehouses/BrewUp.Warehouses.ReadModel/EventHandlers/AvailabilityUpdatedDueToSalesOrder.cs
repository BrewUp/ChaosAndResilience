using BrewUp.Warehouses.ReadModel.Services;
using BrewUp.Warehouses.SharedKernel.Events;
using Microsoft.Extensions.Logging;

namespace BrewUp.Warehouses.ReadModel.EventHandlers;

public sealed class AvailabilityUpdatedDueToSalesOrderEventHandler(ILoggerFactory loggerFactory,
    IAvailabilityService availabilityService)
    : DomainEventHandlerBase<AvailabilityUpdatedDueToSalesOrder>(loggerFactory)
{
    public override async Task HandleAsync(AvailabilityUpdatedDueToSalesOrder @event,
        CancellationToken cancellationToken = new())
    {
        cancellationToken.ThrowIfCancellationRequested();

        await availabilityService.UpdateAvailabilityAsync(@event.BeerId, @event.Quantity, cancellationToken);
    }
}