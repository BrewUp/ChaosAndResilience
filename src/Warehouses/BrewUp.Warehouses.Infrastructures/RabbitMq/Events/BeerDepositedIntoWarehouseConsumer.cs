﻿using BrewUp.Warehouses.ReadModel.EventHandlers;
using BrewUp.Warehouses.ReadModel.Services;
using BrewUp.Warehouses.SharedKernel.Events;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Messages.Events;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUp.Warehouses.Infrastructures.RabbitMq.Events;

public sealed class BeerDepositedIntoWarehouseConsumer(IAvailabilityService availabilityService,
		IEventBus eventBus,
		IMufloneConnectionFactory connectionFactory, ILoggerFactory loggerFactory)
	: DomainEventsConsumerBase<BeerDepositedIntoWarehouse>(connectionFactory, loggerFactory)
{
	protected override IEnumerable<IDomainEventHandlerAsync<BeerDepositedIntoWarehouse>> HandlersAsync { get; } = new List<DomainEventHandlerAsync<BeerDepositedIntoWarehouse>>
	{
		new BeerDepositedIntoWarehouseEventHandler(loggerFactory, availabilityService),
		new BeerDepositedIntoWarehouseForIntegrationEventHandler(loggerFactory, eventBus)
	};
}