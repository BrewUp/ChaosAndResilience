using BrewUp.Warehouses.Domain.CommandHandlers;
using BrewUp.Warehouses.SharedKernel.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUp.Warehouses.Infrastructures.RabbitMq.Commands;

public sealed class RefillBeerIntoWarehouseConsumer(
    IRepository repository,
    IMufloneConnectionFactory connectionFactory,
    ILoggerFactory loggerFactory)
    : CommandConsumerBase<RefillBeerIntoWarehouse>(repository, connectionFactory, loggerFactory)
{
    protected override ICommandHandlerAsync<RefillBeerIntoWarehouse> HandlerAsync { get; } =
        new RefillBeerIntoWarehouseCommandHandler(repository, loggerFactory);
}