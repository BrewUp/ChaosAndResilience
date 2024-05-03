using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainIds;
using BrewUp.Shared.ReadModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BrewUp.Sales.ReadModel.Services;

public sealed class AvailabilityService : ServiceBase, IAvailabilityService
{
	public AvailabilityService(ILoggerFactory loggerFactory, [FromKeyedServices("sales")] IPersister persister) : base(loggerFactory, persister)
	{
	}

	public async Task UpdateAvailabilityAsync(BeerId beerId, BeerName beerName, Quantity quantity,
		CancellationToken cancellationToken = default)
	{
		cancellationToken.ThrowIfCancellationRequested();
		try
		{
			var availability = await Persister.GetByIdAsync<Dtos.Availability>(beerId.Value.ToString(), cancellationToken);
			if (availability == null || string.IsNullOrWhiteSpace(availability.BeerName))
			{
				availability = Dtos.Availability.Create(beerId, beerName, quantity);
				await Persister.InsertAsync(availability, cancellationToken);
			}
			else
			{
				availability.UpdateAvailability(quantity);
				await Persister.UpdateAsync(availability, cancellationToken);
			}
		}
		catch (Exception ex)
		{
			Logger.LogError(ex, "Error updating availability");
			throw;
		}
	}
}