using BrewUp.Sales.Domain.CommandHandlers;
using BrewUp.Sales.SharedKernel.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUp.Sales.Infrastructures.RabbitMQ.Commands;

public class CreateAvailabilityDueToWarehousesNotificationConsumer(IRepository repository,
		IMufloneConnectionFactory connectionFactory,
		ILoggerFactory loggerFactory)
	: CommandConsumerBase<CreateAvailabilityDueToWarehousesNotification>(repository, connectionFactory, loggerFactory)
{
	protected override ICommandHandlerAsync<CreateAvailabilityDueToWarehousesNotification> HandlerAsync { get; } = new CreateAvailabilityDueToWarehousesNotificationCommandHandler(repository, loggerFactory);
}