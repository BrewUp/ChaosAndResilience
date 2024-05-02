using BrewUp.Warehouses.Domain.Entities;
using BrewUp.Warehouses.SharedKernel.Commands;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Persistence;

namespace BrewUp.Warehouses.Domain.CommandHandlers;

public sealed class UpdateAvailabilityDueToSalesOrderCommandHandler : CommandHandlerBaseAsync<UpdateAvailabilityDueToSalesOrder>
{
	public UpdateAvailabilityDueToSalesOrderCommandHandler(IRepository repository, ILoggerFactory loggerFactory) :
		base(repository, loggerFactory)
	{
	}

	public override async Task ProcessCommand(UpdateAvailabilityDueToSalesOrder command, CancellationToken cancellationToken = default)
	{
		try
		{
			var aggregate = await Repository.GetByIdAsync<Availability>(command.BeerId.Value);
            aggregate.UpdateAvailabilityDueToSalesOrder(command.Quantity, command.MessageId);
            await Repository.SaveAsync(aggregate, Guid.NewGuid());
		}
		catch (Exception ex)
		{
            // I'm lazy ... I should raise an event here
            Logger.LogError(ex, "Error Processing command UpdateAvailabilityDueToSalesOrder");
            throw;
		}
	}
}