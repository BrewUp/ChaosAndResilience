using BrewUp.Sales.ReadModel.Services;
using BrewUp.Sales.SharedKernel.Events;
using Microsoft.Extensions.Logging;

namespace BrewUp.Sales.ReadModel.EventHandlers;

public class BeerDueToAvailabilityLoadedCreatedEventHandler
    (ILoggerFactory loggerFactory, IBeersService beersService) : DomainEventHandlerBase<BeerDueToAvailabilityLoadedCreated>(loggerFactory)
{
    public override async Task HandleAsync(BeerDueToAvailabilityLoadedCreated @event, CancellationToken cancellationToken = new())
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        try
        {
            await beersService.CreateBeerAsync(@event.BeerId, @event.BeerName, cancellationToken);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error handling beer due to availability loaded created event");
            throw;
        }
    }    
}