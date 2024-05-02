using BrewUp.Sales.ReadModel.EventHandlers;
using BrewUp.Sales.ReadModel.Services;
using BrewUp.Sales.SharedKernel.Events;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUp.Sales.Infrastructures.RabbitMQ.Events;

public sealed class BeerDueToAvailabilityLoadedCreatedConsumer(IBeersService beersService,
    IMufloneConnectionFactory connectionFactory,
    ILoggerFactory loggerFactory) : DomainEventsConsumerBase<BeerDueToAvailabilityLoadedCreated>(
    connectionFactory, loggerFactory)
{
    protected override IEnumerable<IDomainEventHandlerAsync<BeerDueToAvailabilityLoadedCreated>> HandlersAsync { get; } = new List<IDomainEventHandlerAsync<BeerDueToAvailabilityLoadedCreated>>
    {
        new BeerDueToAvailabilityLoadedCreatedEventHandler(loggerFactory, beersService)
    };
}