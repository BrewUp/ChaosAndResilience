using BrewUp.Sales.ReadModel.EventHandlers;
using BrewUp.Sales.ReadModel.Services;
using BrewUp.Sales.SharedKernel.Events;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Messages.Events;
using Muflone.Transport.Azure.Consumers;
using Muflone.Transport.Azure.Models;

namespace BrewUp.Sales.Infrastructures.Azure.Events;

public sealed class SalesOrderCreatedConsumer : DomainEventConsumerBase<SalesOrderFromPortalCreated>
{
	protected override IEnumerable<IDomainEventHandlerAsync<SalesOrderFromPortalCreated>> HandlersAsync { get; }

	public SalesOrderCreatedConsumer(ISalesOrderService salesOrderService, IEventBus eventBus,
		AzureServiceBusConfiguration azureServiceBusConfiguration, ILoggerFactory loggerFactory) : base(azureServiceBusConfiguration, loggerFactory)
	{
		HandlersAsync = new List<DomainEventHandlerAsync<SalesOrderFromPortalCreated>>
		{
			new SalesOrderCreatedEventHandlerAsync(loggerFactory, salesOrderService)
		};
	}
}