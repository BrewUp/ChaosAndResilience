using BrewUp.Sales.Acl;
using BrewUp.Sales.ReadModel.Dtos;
using BrewUp.Sales.SharedKernel.Events;
using BrewUp.Shared.ReadModel;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUp.Sales.Infrastructures.RabbitMQ.Events;

public sealed class AvailabilityUpdatedForNotificationConsumer(IQueries<Beers> beersQueries,
		IQueries<Availability> availabilityQueries,
		IServiceBus serviceBus,
		IMufloneConnectionFactory connectionFactory, ILoggerFactory loggerFactory)
	: IntegrationEventsConsumerBase<AvailabilityUpdatedForNotification>(connectionFactory, loggerFactory)
{
	protected override IEnumerable<IIntegrationEventHandlerAsync<AvailabilityUpdatedForNotification>> HandlersAsync { get; } = new List<IIntegrationEventHandlerAsync<AvailabilityUpdatedForNotification>>
	{
		new AvailabilityUpdatedForNotificationEventHandler(availabilityQueries, loggerFactory, serviceBus),
		new AvailabilityUpdatedForCreateBeerEventHandler(loggerFactory, serviceBus, beersQueries)
	};
}