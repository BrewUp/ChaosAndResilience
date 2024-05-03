using BrewUp.Sales.Acl;
using BrewUp.Sales.ReadModel.Services;
using BrewUp.Sales.SharedKernel.Events;
using BrewUp.Shared.ReadModel;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUp.Sales.Infrastructures.RabbitMQ.Events;

public sealed class WithdrawlFromWarehouseForSalesOrderConsumer(IQueries<ReadModel.Dtos.Availability> availabilityService,
    IMessagesService messagesService,
    IServiceBus serviceBus,
    IMufloneConnectionFactory connectionFactory, ILoggerFactory loggerFactory)
    : IntegrationEventsConsumerBase<WithdrawlFromWarehouseForSalesOrder>(connectionFactory, loggerFactory)
{
    protected override IEnumerable<IIntegrationEventHandlerAsync<WithdrawlFromWarehouseForSalesOrder>> HandlersAsync { get; } = new List<IIntegrationEventHandlerAsync<WithdrawlFromWarehouseForSalesOrder>>
    {
        new WithdrawlFromWarehouseForSalesOrderEventHandler(availabilityService, loggerFactory, serviceBus, messagesService)
    };
}