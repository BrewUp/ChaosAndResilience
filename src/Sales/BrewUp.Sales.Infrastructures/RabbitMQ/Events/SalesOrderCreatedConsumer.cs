using BrewUp.Sales.ReadModel.EventHandlers;
using BrewUp.Sales.ReadModel.Services;
using BrewUp.Sales.SharedKernel.Events;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Messages.Events;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUp.Sales.Infrastructures.RabbitMQ.Events;

public sealed class SalesOrderCreatedConsumer : DomainEventsConsumerBase<SalesOrderFromPortalCreated>
{
	protected override IEnumerable<IDomainEventHandlerAsync<SalesOrderFromPortalCreated>> HandlersAsync { get; }

	public SalesOrderCreatedConsumer(ISalesOrderService salesOrderService, IEventBus eventBus,
		IMufloneConnectionFactory connectionFactory, ILoggerFactory loggerFactory) : base(connectionFactory, loggerFactory)
	{
		HandlersAsync = new List<DomainEventHandlerAsync<SalesOrderFromPortalCreated>>
		{
			new SalesOrderCreatedEventHandlerAsync(loggerFactory, salesOrderService),
			new SalesOrderCreatedForWarehousesEventHandlerAsync(loggerFactory, eventBus)
		};
	}
}