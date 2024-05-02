﻿using BrewUp.Warehouses.SharedKernel.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUp.Warehouses.Domain.CommandHandlers;

public sealed class DepositBeerIntoWarehouseCommandHandler : CommandHandlerBaseAsync<DepositBeerIntoWarehouse>
{
    public DepositBeerIntoWarehouseCommandHandler(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
    {
    }

    public override async Task ProcessCommand(DepositBeerIntoWarehouse command, CancellationToken cancellationToken = default)
    {
        var aggregate = Entities.Availability.CreateAvailability(command.BeerId, command.BeerName, command.Quantity, command.MessageId);
        await Repository.SaveAsync(aggregate, Guid.NewGuid());
    }
}