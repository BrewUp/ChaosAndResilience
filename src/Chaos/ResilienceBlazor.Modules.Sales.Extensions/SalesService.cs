using ResilienceBlazor.Modules.Sales.Extensions.Dtos;
using ResilienceBlazor.Shared.Configuration;

namespace ResilienceBlazor.Modules.Sales.Extensions;

public sealed class SalesService(ChaosSalesClient chaosSalesClient, ResilienceSalesClient resilienceSalesClient, SalesClient salesClient) : ISalesService
{
	public async Task<PagedResult<SalesOrderJson>>
		GetSalesOrdersWithResilienceAsync(CancellationToken cancellationToken) =>
		await resilienceSalesClient.GetSalesOrdersAsync(cancellationToken);

	public async Task<PagedResult<SalesOrderJson>> GetSalesOrdersWithoutResilienceAsync(CancellationToken cancellationToken) =>
		await chaosSalesClient.GetSalesOrdersAsync(cancellationToken);

	public Task<PagedResult<CustomerJson>> GetCustomersAsync(CancellationToken cancellationToken)
	{
		var customers = new List<CustomerJson>
		{
			new (Guid.NewGuid(), "Il Grottino del Muflone"),
			//new(Guid.NewGuid(), "Il Muflone Assetato"),
			//new(Guid.NewGuid(), "La Birra del Muflone ")
		};

		return Task.FromResult(new PagedResult<CustomerJson>(customers, 1, 10, 3));
	}

	public Task<PagedResult<BeerJson>> GetBeersAsync(CancellationToken cancellationToken) =>
		salesClient.GetBeersAsync(cancellationToken);

	public async Task CreateSalesOrderAsync(SalesOrderJson salesOrder, CancellationToken cancellationToken) =>
			await salesClient.PostSalesOrderAsync(salesOrder, cancellationToken);
}