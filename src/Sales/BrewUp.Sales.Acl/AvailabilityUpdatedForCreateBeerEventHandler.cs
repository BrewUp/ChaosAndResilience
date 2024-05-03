using BrewUp.Sales.ReadModel.Dtos;
using BrewUp.Sales.ReadModel.Services;
using BrewUp.Sales.SharedKernel.Commands;
using BrewUp.Sales.SharedKernel.Events;
using BrewUp.Shared.ReadModel;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Persistence;

namespace BrewUp.Sales.Acl;

public sealed class AvailabilityUpdatedForCreateBeerEventHandler(
	ILoggerFactory loggerFactory,
	IServiceBus serviceBus,
	IMessagesService messagesService,
	IQueries<Beers> queries) : IntegrationEventHandlerAsync<AvailabilityUpdatedForNotification>(loggerFactory)
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
		
		var beer = await queries.GetByIdAsync(@event.BeerId.Value.ToString(), cancellationToken);
		if (beer == null || !string.IsNullOrWhiteSpace(beer.BeerName))
		{
			CreateBeerDueToAvailabilityLoaded command = new(@event.BeerId, correlationId, @event.BeerName);
			await serviceBus.SendAsync(command, cancellationToken);	
		}
	}
}