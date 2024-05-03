using BrewUp.Sales.Domain.Entities;
using BrewUp.Sales.SharedKernel.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUp.Sales.Domain.CommandHandlers;

public sealed class UpdateAvailabilityDueToWarehousesNotificationCommandHandler(
	IRepository repository,
	ILoggerFactory loggerFactory)
	: CommandHandlerBaseAsync<UpdateAvailabilityDueToWarehousesNotification>(repository, loggerFactory)
{
	public override async Task ProcessCommand(UpdateAvailabilityDueToWarehousesNotification command,
		CancellationToken cancellationToken = default)
	{
		var aggregate = await Repository.GetByIdAsync<Availability>(command.BeerId.Value);
		aggregate.UpdateAvailability(command.Quantity, command.MessageId);
	}
}