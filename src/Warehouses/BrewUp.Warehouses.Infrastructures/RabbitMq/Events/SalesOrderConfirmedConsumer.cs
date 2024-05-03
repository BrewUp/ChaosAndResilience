using BrewUp.Warehouses.Acl;
using BrewUp.Warehouses.ReadModel.Services;
using BrewUp.Warehouses.SharedKernel.Events;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUp.Warehouses.Infrastructures.RabbitMq.Events;

public sealed class SalesOrderConfirmedConsumer(IServiceBus serviceBus,
    IMessagesService messagesService,
    IMufloneConnectionFactory connectionFactory, ILoggerFactory loggerFactory, ISerializer? messageSerializer = null) : IntegrationEventsConsumerBase<SalesOrderConfirmed>(connectionFactory, loggerFactory)
{
    protected override IEnumerable<IIntegrationEventHandlerAsync<SalesOrderConfirmed>> HandlersAsync { get; } =
        new List<IIntegrationEventHandlerAsync<SalesOrderConfirmed>>
        {
            new SalesOrderConfirmedEventHandler(loggerFactory, serviceBus, messagesService)
        };
}