using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainIds;
using BrewUp.Warehouses.SharedKernel.Events;
using Muflone.Core;

namespace BrewUp.Warehouses.Domain.Entities;

public class Availability : AggregateRoot
{
	internal BeerId _beerId;
	internal BeerName _beerName;
	internal Quantity _quantity;

	protected Availability()
	{
	}

	internal static Availability CreateAvailability(BeerId beerId, BeerName beerName, Quantity quantity, Guid correlationId)
	{
		return new Availability(beerId, beerName, quantity, correlationId);
	}

	private Availability(BeerId beerId, BeerName beerName, Quantity quantity, Guid correlationId)
	{
		RaiseEvent(new BeerDepositedIntoWarehouse(beerId, correlationId, beerName, quantity));
	}

	private void Apply(BeerDepositedIntoWarehouse @event)
	{
		Id = @event.BeerId;

		_beerId = @event.BeerId;
		_beerName = @event.BeerName;
		_quantity = @event.Quantity;
	}

	internal void UpdateAvailabilityDueToSalesOrder(Quantity quantity, Guid correlationId)
	{
		quantity = _quantity with { Value = _quantity.Value - quantity.Value };
		RaiseEvent(new AvailabilityUpdatedDueToSalesOrder(_beerId, correlationId, quantity, _beerName));
	}

	private void Apply(AvailabilityUpdatedDueToSalesOrder @event)
	{
		_quantity = @event.Quantity;
	}
}