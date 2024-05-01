using BrewUp.Sales.SharedKernel.Events;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainIds;
using Muflone.Core;

namespace BrewUp.Sales.Domain.Entities;

public class Beers : AggregateRoot
{
    private BeerName _beerName;
    
    protected Beers()
    {}
    
    internal static Beers CreateBeers(BeerId beerId, BeerName beerName, Guid correlationId) => new(beerId, beerName, correlationId);
    
    private Beers(BeerId beerId, BeerName beerName, Guid correlationId)
    {
        RaiseEvent(new BeerDueToAvailabilityLoadedCreated(beerId, correlationId, beerName));
    }

    private void Apply(BeerDueToAvailabilityLoadedCreated @event)
    {
        Id = @event.BeerId;
        
        _beerName = @event.BeerName;
    }
}