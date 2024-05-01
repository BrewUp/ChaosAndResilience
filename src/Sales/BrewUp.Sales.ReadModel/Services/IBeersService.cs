using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainIds;
using BrewUp.Shared.Entities;

namespace BrewUp.Sales.ReadModel.Services;

public interface IBeersService
{
    Task CreateBeerAsync(BeerId beerId, BeerName beerName, CancellationToken cancellationToken = default);
    Task<PagedResult<BeerJson>> GetBeersAsync(int page, int pageSize, CancellationToken cancellationToken = default);
}