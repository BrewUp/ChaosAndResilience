using BrewUp.Warehouses.Domain.CommandHandlers;
using BrewUp.Warehouses.SharedKernel.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUp.Warehouses.Infrastructures.RabbitMq.Commands;

public sealed class UpdateAvailabilityDueToSalesOrderConsumer(IRepository repository,
    IMufloneConnectionFactory connectionFactory,
    ILoggerFactory loggerFactory)
    : CommandConsumerBase<UpdateAvailabilityDueToSalesOrder>(repository, connectionFactory, loggerFactory)
{
    protected override ICommandHandlerAsync<UpdateAvailabilityDueToSalesOrder> HandlerAsync { get; } =
        new UpdateAvailabilityDueToSalesOrderCommandHandler(repository, loggerFactory);
}