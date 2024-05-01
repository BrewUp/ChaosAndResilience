using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainIds;
using Muflone.Messages.Events;

namespace BrewUp.Sales.SharedKernel.Events;

public sealed class BeerDueToAvailabilityLoadedCreated(BeerId aggregateId, Guid commitId, BeerName beerName) : DomainEvent(aggregateId, commitId)
{
    public readonly BeerId BeerId = aggregateId;
    public readonly BeerName BeerName = beerName;
}