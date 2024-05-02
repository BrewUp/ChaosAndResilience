using BrewUp.Warehouses.SharedKernel.Events;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Persistence;
using Muflone.Transport.Azure.Consumers;
using Muflone.Transport.Azure.Models;

namespace BrewUp.Warehouses.Infrastructures.Azure.Events;

public sealed class SalesOrderConfirmedConsumer : IntegrationEventConsumerBase<SalesOrderConfirmed>
{
    public SalesOrderConfirmedConsumer(AzureServiceBusConfiguration azureServiceBusConfiguration, ILoggerFactory loggerFactory, ISerializer? messageSerializer = null) : base(azureServiceBusConfiguration, loggerFactory, messageSerializer)
    {
    }

    protected override IEnumerable<IIntegrationEventHandlerAsync<SalesOrderConfirmed>> HandlersAsync { get; }
}