﻿using BrewUp.Shared.DomainIds;
using BrewUp.Warehouses.SharedKernel.Commands;
using BrewUp.Warehouses.SharedKernel.Events;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Persistence;

namespace BrewUp.Warehouses.Acl;

public sealed class SalesOrderConfirmedEventHandler(ILoggerFactory loggerFactory,
    IServiceBus serviceBus) : IntegrationEventHandlerAsync<SalesOrderConfirmed>(loggerFactory)
{
    public override async Task HandleAsync(SalesOrderConfirmed @event, CancellationToken cancellationToken = new ())
    {
        cancellationToken.ThrowIfCancellationRequested();

        var correlationId =
            new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);

        foreach (var row in @event.Rows)
        {
            UpdateAvailabilityDueToSalesOrder command = new(new BeerId(row.BeerId), correlationId, row.Quantity);
            await serviceBus.SendAsync(command, cancellationToken);
        }
    }
}