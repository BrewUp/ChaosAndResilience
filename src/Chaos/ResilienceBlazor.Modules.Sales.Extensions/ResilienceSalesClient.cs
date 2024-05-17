using Polly.Registry;
using ResilienceBlazor.Modules.Sales.Extensions.Dtos;
using ResilienceBlazor.Shared.Configuration;
using System.Net.Http.Json;

namespace ResilienceBlazor.Modules.Sales.Extensions;

public class ResilienceSalesClient(HttpClient client,
	ResiliencePipelineProvider<string> resiliencePipelineProvider,
	ISalesService salesService)
{
	public async Task<PagedResult<SalesOrderJson>> GetSalesOrdersAsync(CancellationToken cancellationToken)
	{
		var pipeline =
			resiliencePipelineProvider.GetPipeline<PagedResult<SalesOrderJson>>("brewup-getsalesorders-resilience");
		return await pipeline.ExecuteAsync(async token => await salesService.GetSalesOrdersWithResilienceAsync(token), cancellationToken);
	}

	public async Task PostSalesOrderAsync(SalesOrderJson salesOrder, CancellationToken cancellationToken)
	{
		var pipeline =
			resiliencePipelineProvider.GetPipeline("brewup-postsalesorders-resilience");
		await pipeline.ExecuteAsync(async token =>
			await salesService.CreateSalesOrderAsync(salesOrder,
				token), cancellationToken);
	}

	public async Task<PagedResult<BeerJson>> GetBeersAsync(CancellationToken cancellationToken)
		=> await client.GetFromJsonAsync<PagedResult<BeerJson>>("v1/availabilities", cancellationToken)
		   ?? new PagedResult<BeerJson>(Enumerable.Empty<BeerJson>(), 0, 0, 0);
}