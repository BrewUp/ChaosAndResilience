using BrewUp.Sales.Domain.CommandHandlers;
using BrewUp.Sales.SharedKernel.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.Azure.Consumers;
using Muflone.Transport.Azure.Models;

namespace BrewUp.Sales.Infrastructures.Azure.Commands;

public sealed class CreateSalesOrderConsumer(IRepository repository,
		AzureServiceBusConfiguration azureServiceBusConfiguration,
		ILoggerFactory loggerFactory)
	: CommandConsumerBase<CreateSalesOrderFromPortal>(azureServiceBusConfiguration, loggerFactory)
{
	protected override ICommandHandlerAsync<CreateSalesOrderFromPortal> HandlerAsync { get; } = new CreateSalesOrderCommandHandler(repository, loggerFactory);
}