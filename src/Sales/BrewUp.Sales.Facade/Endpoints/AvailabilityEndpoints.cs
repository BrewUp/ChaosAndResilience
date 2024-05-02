using BrewUp.Sales.ReadModel.Dtos;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.Entities;
using BrewUp.Shared.ReadModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace BrewUp.Sales.Facade.Endpoints;

public static class AvailabilityEndpoints
{
	public static IEndpointRouteBuilder MapAvailabilityEndpoints(this IEndpointRouteBuilder endpoints)
	{
		var group = endpoints.MapGroup("/v1/availabilities/")
			.WithTags("Availabilities");

		group.MapGet("/", HandleGetBeers)
			.Produces(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status200OK)
			.WithName("GetBeers");

		return endpoints;
	}

	private static async Task<IResult> HandleGetBeers(
		IQueries<Availability> queries,
		CancellationToken cancellationToken)
	{
		var beers = await queries.GetByFilterAsync(null, 0, 10, cancellationToken);

		return beers.TotalRecords > 0
			? Results.Ok(new PagedResult<BeerAvailabilityJson>(beers.Results.Select(b => b.ToJson()), beers.Page, beers.PageSize, beers.TotalRecords))
			: Results.Ok(new PagedResult<BeerAvailabilityJson>(Enumerable.Empty<BeerAvailabilityJson>(), 0, 0, 0));
	}
}