using BrewUp.Sales.Facade.Validators;
using BrewUp.Shared.Configurations;
using BrewUp.Shared.Contracts;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace BrewUp.Sales.Facade.Endpoints;

public static class SalesEndpoints
{
	public static IEndpointRouteBuilder MapSalesEndpoints(this IEndpointRouteBuilder endpoints)
	{
		var group = endpoints.MapGroup("/v1/sales/")
			.WithTags("Sales");

		group.MapPost("/", HandleCreateOrder)
			.Produces(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status201Created)
			.WithName("CreateSalesOrder");

		if (!string.IsNullOrWhiteSpace(RateLimiting.CurrentRateLimiter))
		{
			group.MapGet("/", HandleGetOrders)
				.Produces(StatusCodes.Status400BadRequest)
				.Produces(StatusCodes.Status200OK)
				.RequireRateLimiting(RateLimiting.CurrentRateLimiter)
				.WithName("GetSalesOrders");	
		}
		else
		{
			group.MapGet("/", HandleGetOrders)
				.Produces(StatusCodes.Status400BadRequest)
				.Produces(StatusCodes.Status200OK)
				.WithName("GetSalesOrders");	
		}
		

		return endpoints;
	}

	private static async Task<IResult> HandleCreateOrder(
		ISalesFacade salesUpFacade,
		IValidator<SalesOrderJson> validator,
		ValidationHandler validationHandler,
		SalesOrderJson body,
		CancellationToken cancellationToken)
	{
		await validationHandler.ValidateAsync(validator, body);
		if (!validationHandler.IsValid)
			return Results.BadRequest(validationHandler.Errors);

		var salesOrderId = await salesUpFacade.CreateOrderAsync(body, cancellationToken);

		return string.IsNullOrWhiteSpace(salesOrderId) 
			? Results.Ok("No Beers Available") 
			: Results.Created(new Uri($"/v1/sales/{salesOrderId}", UriKind.Relative), salesOrderId);
	}

	private static async Task<IResult> HandleGetOrders(
		ISalesFacade salesUpFacade,
		CancellationToken cancellationToken)
	{
		var orders = await salesUpFacade.GetOrdersAsync(cancellationToken);

		return Results.Ok(orders);
	}
}