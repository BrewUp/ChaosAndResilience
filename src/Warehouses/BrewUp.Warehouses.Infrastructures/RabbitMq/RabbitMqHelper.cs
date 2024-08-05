using BrewUp.Warehouses.Infrastructures.RabbitMq.Commands;
using BrewUp.Warehouses.Infrastructures.RabbitMq.Events;
using BrewUp.Warehouses.ReadModel.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Factories;
using Muflone.Transport.RabbitMQ.Models;

namespace BrewUp.Warehouses.Infrastructures.RabbitMq;

public static class RabbitMqHelper
{
	public static IServiceCollection AddRabbitMqForWarehousesModule(this IServiceCollection services,
		RabbitMqSettings rabbitMqSettings)
	{
		var serviceProvider = services.BuildServiceProvider();
		var repository = serviceProvider.GetRequiredService<IRepository>();
		var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

		var rabbitMqConfiguration = new RabbitMQConfiguration(rabbitMqSettings.Host, rabbitMqSettings.Username,
			rabbitMqSettings.Password, rabbitMqSettings.ExchangeCommandName, rabbitMqSettings.ExchangeEventName,
			"warehouses");
		var mufloneConnectionFactory = new MufloneConnectionFactory(rabbitMqConfiguration, loggerFactory);

		services.AddMufloneTransportRabbitMQ(loggerFactory, rabbitMqConfiguration);

		serviceProvider = services.BuildServiceProvider();
		var consumers = serviceProvider.GetRequiredService<IEnumerable<IConsumer>>();
		consumers = consumers.Concat(new List<IConsumer>
		{
			new DepositBeerIntoWarehouseConsumer(repository, mufloneConnectionFactory, loggerFactory),
			new BeerDepositedIntoWarehouseConsumer(serviceProvider.GetRequiredService<IAvailabilityService>(),
				serviceProvider.GetRequiredService<IEventBus>(),
				mufloneConnectionFactory, loggerFactory),
			
			new RefillBeerIntoWarehouseConsumer(repository, mufloneConnectionFactory, loggerFactory),
			new BeerRefilledIntoWarehouseConsumer(loggerFactory, serviceProvider.GetRequiredService<IEventBus>(), 
				mufloneConnectionFactory, serviceProvider.GetRequiredService<IAvailabilityService>()),
			
			new UpdateAvailabilityDueToSalesOrderConsumer(repository, mufloneConnectionFactory, loggerFactory),
			new AvailabilityUpdatedDueToSalesOrderConsumer(serviceProvider.GetRequiredService<IEventBus>(),
				loggerFactory, mufloneConnectionFactory, 
				serviceProvider.GetRequiredService<IAvailabilityService>()),
			
			new SalesOrderConfirmedConsumer(serviceProvider.GetRequiredService<IServiceBus>(),
				serviceProvider.GetRequiredService<IMessagesService>(),
				mufloneConnectionFactory, loggerFactory)
		});
		services.AddMufloneRabbitMQConsumers(consumers);

		return services;
	}
}