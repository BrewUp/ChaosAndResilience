using BrewUp.Warehouses.ReadModel.EventHandlers;
using BrewUp.Warehouses.ReadModel.Services;
using BrewUp.Warehouses.SharedKernel.Events;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Messages.Events;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUp.Warehouses.Infrastructures.RabbitMq.Events;

public sealed class AvailabilityUpdatedDueToSalesOrderConsumer(IEventBus eventBus,
    ILoggerFactory loggerFactory,
    IMufloneConnectionFactory connectionFactory,
    IAvailabilityService availabilityService) : DomainEventsConsumerBase<AvailabilityUpdatedDueToSalesOrder>(connectionFactory, loggerFactory)
{
    protected override IEnumerable<IDomainEventHandlerAsync<AvailabilityUpdatedDueToSalesOrder>> HandlersAsync { get; } = new List<DomainEventHandlerAsync<AvailabilityUpdatedDueToSalesOrder>>
    {
        new AvailabilityUpdatedDueToSalesOrderEventHandler(loggerFactory, availabilityService),
        new AvailabilityUpdatedDueToSalesOrderForIntegrationEventHandler(loggerFactory, eventBus)
    };
}