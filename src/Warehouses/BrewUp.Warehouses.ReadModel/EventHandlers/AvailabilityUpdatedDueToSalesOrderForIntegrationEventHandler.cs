using BrewUp.Warehouses.ReadModel.Services;
using BrewUp.Warehouses.SharedKernel.Events;
using Microsoft.Extensions.Logging;
using Muflone;

namespace BrewUp.Warehouses.ReadModel.EventHandlers;

public sealed class AvailabilityUpdatedDueToSalesOrderForIntegrationEventHandler(ILoggerFactory loggerFactory,
    IEventBus eventBus)
    : DomainEventHandlerBase<AvailabilityUpdatedDueToSalesOrder>(loggerFactory)
{
    public override async Task HandleAsync(AvailabilityUpdatedDueToSalesOrder @event,
        CancellationToken cancellationToken = new())
    {
        cancellationToken.ThrowIfCancellationRequested();

        var correlationId =
            new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);

        WithdrawlFromWarehouseForSalesOrder integrationEvent = new(@event.BeerId, correlationId, @event.BeerName, @event.Quantity);
        await eventBus.PublishAsync(integrationEvent, cancellationToken);
    }
}