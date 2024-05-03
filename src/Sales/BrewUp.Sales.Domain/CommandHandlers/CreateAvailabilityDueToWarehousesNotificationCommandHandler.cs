using BrewUp.Sales.Domain.Entities;
using BrewUp.Sales.SharedKernel.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUp.Sales.Domain.CommandHandlers;

public sealed class CreateAvailabilityDueToWarehousesNotificationCommandHandler(
	IRepository repository,
	ILoggerFactory loggerFactory)
	: CommandHandlerBaseAsync<CreateAvailabilityDueToWarehousesNotification>(repository, loggerFactory)
{
	public override async Task ProcessCommand(CreateAvailabilityDueToWarehousesNotification command,
		CancellationToken cancellationToken = default)
	{
		var aggregate = Availability.CreateAvailability(command.BeerId, command.BeerName, command.Quantity, command.MessageId);
		await Repository.SaveAsync(aggregate, Guid.NewGuid());
	}
}