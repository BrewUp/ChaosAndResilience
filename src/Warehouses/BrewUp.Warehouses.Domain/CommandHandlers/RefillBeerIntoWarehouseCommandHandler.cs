using BrewUp.Warehouses.Domain.Entities;
using BrewUp.Warehouses.SharedKernel.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUp.Warehouses.Domain.CommandHandlers;

public sealed class RefillBeerIntoWarehouseCommandHandler(IRepository repository, ILoggerFactory loggerFactory)
    : CommandHandlerBaseAsync<RefillBeerIntoWarehouse>(repository, loggerFactory)
{
    public override async Task ProcessCommand(RefillBeerIntoWarehouse command, CancellationToken cancellationToken = default)
    {
        var aggregate = await Repository.GetByIdAsync<Availability>(command.BeerId.Value);
        aggregate.RefillBeer(command.Quantity, command.MessageId);
        await Repository.SaveAsync(aggregate, Guid.NewGuid());
    }
}