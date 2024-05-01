using BrewUp.Sales.Domain.CommandHandlers;
using BrewUp.Sales.SharedKernel.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.Azure.Consumers;
using Muflone.Transport.Azure.Models;

namespace BrewUp.Sales.Infrastructures.Azure.Commands;

public sealed class CreateBeerDueToAvailabilityLoadedConsumer(IRepository repository,
    AzureServiceBusConfiguration azureServiceBusConfiguration,
    ILoggerFactory loggerFactory) : CommandConsumerBase<CreateBeerDueToAvailabilityLoaded>(azureServiceBusConfiguration, loggerFactory)
{
    protected override ICommandHandlerAsync<CreateBeerDueToAvailabilityLoaded> HandlerAsync { get; } =
        new CreateBeerDueToAvailabilityLoadedCommandHandler(repository, loggerFactory);
}