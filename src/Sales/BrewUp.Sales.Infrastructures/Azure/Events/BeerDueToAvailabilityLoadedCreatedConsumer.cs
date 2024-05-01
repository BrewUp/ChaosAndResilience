using BrewUp.Sales.ReadModel.EventHandlers;
using BrewUp.Sales.ReadModel.Services;
using BrewUp.Sales.SharedKernel.Events;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Transport.Azure.Consumers;
using Muflone.Transport.Azure.Models;

namespace BrewUp.Sales.Infrastructures.Azure.Events;

public sealed class BeerDueToAvailabilityLoadedCreatedConsumer(IBeersService beersService,
    AzureServiceBusConfiguration azureServiceBusConfiguration,
    ILoggerFactory loggerFactory) : DomainEventConsumerBase<BeerDueToAvailabilityLoadedCreated>(
    azureServiceBusConfiguration, loggerFactory)
{
    protected override IEnumerable<IDomainEventHandlerAsync<BeerDueToAvailabilityLoadedCreated>> HandlersAsync { get; } = new List<IDomainEventHandlerAsync<BeerDueToAvailabilityLoadedCreated>>
    {
        new BeerDueToAvailabilityLoadedCreatedEventHandler(loggerFactory, beersService)
    };
}