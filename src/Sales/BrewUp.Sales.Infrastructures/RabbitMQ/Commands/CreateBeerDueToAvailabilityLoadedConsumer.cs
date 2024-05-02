using BrewUp.Sales.Domain.CommandHandlers;
using BrewUp.Sales.SharedKernel.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUp.Sales.Infrastructures.RabbitMQ.Commands;

public sealed class CreateBeerDueToAvailabilityLoadedConsumer(IRepository repository,
    IMufloneConnectionFactory connectionFactory,
    ILoggerFactory loggerFactory) : CommandConsumerBase<CreateBeerDueToAvailabilityLoaded>(repository, connectionFactory, loggerFactory)
{
    protected override ICommandHandlerAsync<CreateBeerDueToAvailabilityLoaded> HandlerAsync { get; } =
        new CreateBeerDueToAvailabilityLoadedCommandHandler(repository, loggerFactory);
}