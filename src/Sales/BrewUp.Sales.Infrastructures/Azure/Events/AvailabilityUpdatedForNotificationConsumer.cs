using BrewUp.Sales.Acl;
using BrewUp.Sales.ReadModel.Dtos;
using BrewUp.Sales.SharedKernel.Events;
using BrewUp.Shared.ReadModel;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Persistence;
using Muflone.Transport.Azure.Consumers;
using Muflone.Transport.Azure.Models;

namespace BrewUp.Sales.Infrastructures.Azure.Events;

public sealed class AvailabilityUpdatedForNotificationConsumer(IQueries<Beers> beersQueries,
		IQueries<Availability> availabilityQueries,
		IServiceBus serviceBus,
		AzureServiceBusConfiguration azureServiceBusConfiguration, ILoggerFactory loggerFactory)
	: IntegrationEventConsumerBase<AvailabilityUpdatedForNotification>(azureServiceBusConfiguration, loggerFactory)
{
	protected override IEnumerable<IIntegrationEventHandlerAsync<AvailabilityUpdatedForNotification>> HandlersAsync { get; } = new List<IIntegrationEventHandlerAsync<AvailabilityUpdatedForNotification>>
	{
		new AvailabilityUpdatedForNotificationEventHandler(availabilityQueries, loggerFactory, serviceBus),
		new AvailabilityUpdatedForCreateBeerEventHandler(loggerFactory, serviceBus, beersQueries)
	};
}