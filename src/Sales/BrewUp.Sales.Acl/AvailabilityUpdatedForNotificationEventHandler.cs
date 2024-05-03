using BrewUp.Sales.ReadModel.Dtos;
using BrewUp.Sales.ReadModel.Services;
using BrewUp.Sales.SharedKernel.Commands;
using BrewUp.Sales.SharedKernel.Events;
using BrewUp.Shared.ReadModel;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Persistence;

namespace BrewUp.Sales.Acl;

public sealed class AvailabilityUpdatedForNotificationEventHandler(IQueries<Availability> queries,
	ILoggerFactory loggerFactory,
	IServiceBus serviceBus,
	IMessagesService messagesService) : IntegrationEventHandlerAsync<AvailabilityUpdatedForNotification>(loggerFactory)
{
	public override async Task HandleAsync(AvailabilityUpdatedForNotification @event,
		CancellationToken cancellationToken = new())
	{
		cancellationToken.ThrowIfCancellationRequested();
		
		var isMessageProcessed =
			await messagesService.IsMessageProcessedAsync(@event.MessageId, @event.GetType().FullName!, @event.Version,
				DateTime.UtcNow);
		
		if (isMessageProcessed)
			return;

		var correlationId =
			new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);

		var availability = await queries.GetByIdAsync(@event.BeerId.Value.ToString(), cancellationToken);
		if (availability == null || string.IsNullOrWhiteSpace(availability.BeerName))
		{
			CreateAvailabilityDueToWarehousesNotification command = new(@event.BeerId, correlationId, @event.BeerName, @event.Quantity);
			await serviceBus.SendAsync(command, cancellationToken);
		}
		else
		{
			UpdateAvailabilityDueToWarehousesNotification command = new(@event.BeerId, correlationId, @event.BeerName, @event.Quantity);
			await serviceBus.SendAsync(command, cancellationToken);
		}
	}
}