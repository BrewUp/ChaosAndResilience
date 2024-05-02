using BrewUp.Warehouses.Domain.CommandHandlers;
using BrewUp.Warehouses.SharedKernel.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUp.Warehouses.Infrastructures.RabbitMq.Commands;

public sealed class DepositBeerIntoWarehouseConsumer(IRepository repository,
    IMufloneConnectionFactory connectionFactory,
    ILoggerFactory loggerFactory)
    : CommandConsumerBase<DepositBeerIntoWarehouse>(repository, connectionFactory, loggerFactory)
{
    protected override ICommandHandlerAsync<DepositBeerIntoWarehouse> HandlerAsync { get; } =
        new DepositBeerIntoWarehouseCommandHandler(repository, loggerFactory);
}