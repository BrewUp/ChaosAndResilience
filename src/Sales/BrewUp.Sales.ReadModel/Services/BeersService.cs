using BrewUp.Sales.ReadModel.Dtos;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainIds;
using BrewUp.Shared.Entities;
using BrewUp.Shared.ReadModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BrewUp.Sales.ReadModel.Services;

public sealed class BeersService(ILoggerFactory loggerFactory, [FromKeyedServices("sales")] IPersister persister, IQueries<Beers> queries)
    : ServiceBase(loggerFactory, persister), IBeersService
{

    public async Task CreateBeerAsync(BeerId beerId, BeerName beerName, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        try
        {
            var beer = Beers.CreateBeer(beerId, beerName);
            await Persister.InsertAsync(beer, cancellationToken);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error updating availability");
            throw;
        }
    }

    public async Task<PagedResult<BeerJson>> GetBeersAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            var beers = await queries.GetByFilterAsync(null, page, pageSize, cancellationToken);
            
            return beers.TotalRecords > 0
                ? new PagedResult<BeerJson>(beers.Results.Select(r => r.ToJson()), beers.Page, beers.PageSize, beers.TotalRecords)
                : new PagedResult<BeerJson>(Enumerable.Empty<BeerJson>(), 0, 0, 0);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error reading Beers");
            throw;
        }
    }
}