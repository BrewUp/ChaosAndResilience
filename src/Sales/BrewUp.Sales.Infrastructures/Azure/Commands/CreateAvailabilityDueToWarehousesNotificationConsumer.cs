using BrewUp.Sales.Domain.CommandHandlers;
using BrewUp.Sales.SharedKernel.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.Azure.Consumers;
using Muflone.Transport.Azure.Models;

namespace BrewUp.Sales.Infrastructures.Azure.Commands;

public class CreateAvailabilityDueToWarehousesNotificationConsumer(IRepository repository,
		AzureServiceBusConfiguration azureServiceBusConfiguration,
		ILoggerFactory loggerFactory)
	: CommandConsumerBase<CreateAvailabilityDueToWarehousesNotification>(azureServiceBusConfiguration, loggerFactory)
{
	protected override ICommandHandlerAsync<CreateAvailabilityDueToWarehousesNotification> HandlerAsync { get; } = new CreateAvailabilityDueToWarehousesNotificationCommandHandler(repository, loggerFactory);
}