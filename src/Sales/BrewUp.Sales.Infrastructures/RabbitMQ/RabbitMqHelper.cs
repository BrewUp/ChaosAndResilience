using BrewUp.Sales.Infrastructures.RabbitMQ.Commands;
using BrewUp.Sales.Infrastructures.RabbitMQ.Events;
using BrewUp.Sales.ReadModel.Dtos;
using BrewUp.Sales.ReadModel.Services;
using BrewUp.Shared.ReadModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Factories;
using Muflone.Transport.RabbitMQ.Models;

namespace BrewUp.Sales.Infrastructures.RabbitMQ;

public static class RabbitMqHelper
{
	public static IServiceCollection AddAzureForSalesModule(this IServiceCollection services,
		RabbitMqSettings rabbitMqSettings)
	{
		var serviceProvider = services.BuildServiceProvider();
		var repository = serviceProvider.GetRequiredService<IRepository>();
		var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

		var rabbitMqConfiguration = new RabbitMQConfiguration(rabbitMqSettings.Host, rabbitMqSettings.Username,
			rabbitMqSettings.Password, rabbitMqSettings.ExchangeCommandName, rabbitMqSettings.ExchangeEventName,
			"sales");
		var mufloneConnectionFactory = new MufloneConnectionFactory(rabbitMqConfiguration, loggerFactory);
		services.AddMufloneTransportRabbitMQ(loggerFactory, rabbitMqConfiguration);

		serviceProvider = services.BuildServiceProvider();
		var consumers = serviceProvider.GetRequiredService<IEnumerable<IConsumer>>();
		consumers = consumers.Concat(new List<IConsumer>
		{
			new CreateSalesOrderConsumer(repository,
				mufloneConnectionFactory,
				loggerFactory),
			new SalesOrderCreatedConsumer(serviceProvider.GetRequiredService<ISalesOrderService>(),
				serviceProvider.GetRequiredService<IEventBus>(),
				mufloneConnectionFactory, loggerFactory),

			new AvailabilityUpdatedForNotificationConsumer(serviceProvider.GetRequiredService<IQueries<Beers>>(),
				serviceProvider.GetRequiredService<IQueries<Availability>>(),
				serviceProvider.GetRequiredService<IServiceBus>(),
				serviceProvider.GetRequiredService<IMessagesService>(),
				mufloneConnectionFactory,
				loggerFactory),
			
			new CreateAvailabilityDueToWarehousesNotificationConsumer(repository, mufloneConnectionFactory,
				loggerFactory),
			new UpdateAvailabilityDueToWarehousesNotificationConsumer(repository, mufloneConnectionFactory,
				loggerFactory),
			new AvailabilityUpdatedDueToWarehousesNotificationConsumer(serviceProvider.GetRequiredService<IAvailabilityService>(),
				mufloneConnectionFactory, loggerFactory),
			new WithdrawlFromWarehouseForSalesOrderConsumer(serviceProvider.GetRequiredService<IQueries<ReadModel.Dtos.Availability>>(),
				serviceProvider.GetRequiredService<IMessagesService>(),
				serviceProvider.GetRequiredService<IServiceBus>(),
				mufloneConnectionFactory, loggerFactory),
			
			new CreateBeerDueToAvailabilityLoadedConsumer(repository, mufloneConnectionFactory, loggerFactory),
			new BeerDueToAvailabilityLoadedCreatedConsumer(serviceProvider.GetRequiredService<IBeersService>(), 
				mufloneConnectionFactory, loggerFactory)
		});

		services.AddMufloneRabbitMQConsumers(consumers);

		return services;
	}
}