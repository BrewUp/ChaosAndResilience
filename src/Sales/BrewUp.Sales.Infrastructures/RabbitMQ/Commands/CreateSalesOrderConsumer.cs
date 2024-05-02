using BrewUp.Sales.Domain.CommandHandlers;
using BrewUp.Sales.SharedKernel.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUp.Sales.Infrastructures.RabbitMQ.Commands;

public sealed class CreateSalesOrderConsumer(IRepository repository,
		IMufloneConnectionFactory connectionFactory,
		ILoggerFactory loggerFactory)
	: CommandConsumerBase<CreateSalesOrderFromPortal>(repository, connectionFactory, loggerFactory)
{
	protected override ICommandHandlerAsync<CreateSalesOrderFromPortal> HandlerAsync { get; } = new CreateSalesOrderCommandHandler(repository, loggerFactory);
}