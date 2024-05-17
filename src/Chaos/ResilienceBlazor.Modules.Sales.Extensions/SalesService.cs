using Microsoft.Extensions.Logging;
using ResilienceBlazor.Modules.Sales.Extensions.Dtos;
using ResilienceBlazor.Shared.Abstracts;
using ResilienceBlazor.Shared.Concretes;
using ResilienceBlazor.Shared.Configuration;

namespace ResilienceBlazor.Modules.Sales.Extensions;

public sealed class SalesService(
	HttpClient httpClient,
	IHttpService httpService,
	AppConfiguration appConfiguration,
	ILoggerFactory loggerFactory)
	: BaseHttpService(httpClient, httpService, appConfiguration, loggerFactory), ISalesService
{
	public async Task<PagedResult<SalesOrderJson>>
		GetSalesOrdersWithResilienceAsync(CancellationToken cancellationToken)
	{
		return await HttpService.Get<PagedResult<SalesOrderJson>>(
				$"{AppConfiguration.BrewUpSalesUri}v1/sales");
	}

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

	public async Task<PagedResult<BeerJson>> GetBeersAsync(CancellationToken cancellationToken) =>
		await HttpService.Get<PagedResult<BeerJson>>(
			$"{AppConfiguration.BrewUpSalesUri}v1/availabilities");

	public Task CreateSalesOrderAsync(SalesOrderJson salesOrder, CancellationToken cancellationToken) =>
			Task.CompletedTask;
}