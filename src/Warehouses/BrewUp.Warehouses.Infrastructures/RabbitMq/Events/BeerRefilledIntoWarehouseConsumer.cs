using BrewUp.Warehouses.ReadModel.EventHandlers;
using BrewUp.Warehouses.ReadModel.Services;
using BrewUp.Warehouses.SharedKernel.Events;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Messages.Events;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUp.Warehouses.Infrastructures.RabbitMq.Events;

public sealed class BeerRefilledIntoWarehouseConsumer(ILoggerFactory loggerFactory,
    IEventBus eventBus,
    IMufloneConnectionFactory connectionFactory, 
    IAvailabilityService availabilityService) : DomainEventsConsumerBase<BeerRefilledIntoWarehouse>(connectionFactory, loggerFactory)
{
    protected override IEnumerable<IDomainEventHandlerAsync<BeerRefilledIntoWarehouse>> HandlersAsync { get; } = new List<DomainEventHandlerAsync<BeerRefilledIntoWarehouse>>
    {
        new BeerRefilledIntoWarehouseForIntegrationEventHandler(loggerFactory, eventBus),
        new BeerRefilledIntoWarehouseEventHandler(loggerFactory, availabilityService)
    };
}