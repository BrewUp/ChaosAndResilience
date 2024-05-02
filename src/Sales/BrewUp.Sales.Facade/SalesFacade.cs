﻿using BrewUp.Sales.ReadModel.Dtos;
using BrewUp.Sales.SharedKernel.Commands;
using BrewUp.Sales.SharedKernel.CustomTypes;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainIds;
using BrewUp.Shared.Entities;
using BrewUp.Shared.ReadModel;
using Muflone.Persistence;
using Availability = BrewUp.Sales.ReadModel.Dtos.Availability;

namespace BrewUp.Sales.Facade;

public sealed class SalesFacade(IServiceBus serviceBus,
	IQueries<SalesOrder> orderQueries,
	IQueries<Availability> availabilityQueries) : ISalesFacade
{
	public async Task<string> CreateOrderAsync(SalesOrderJson body, CancellationToken cancellationToken)
	{
		if (body.SalesOrderId.Equals(string.Empty))
			body = body with { SalesOrderId = Guid.NewGuid().ToString() };

		// var beersAvailable = await GetSalesOrderRows(body, cancellationToken);
		//
		// if (!beersAvailable.Any())
		// 	return string.Empty;

		CreateSalesOrderFromPortal command = new(new SalesOrderId(new Guid(body.SalesOrderId)), Guid.NewGuid(),
			new SalesOrderNumber(body.SalesOrderNumber), new OrderDate(body.OrderDate), new CustomerId(body.CustomerId),
			new CustomerName(body.CustomerName), body.PaymentDetails, body.DeliveryAddress, body.Rows);
		await serviceBus.SendAsync(command, cancellationToken);

		return body.SalesOrderId;
	}

	private async Task<List<SalesOrderRowJson>> GetSalesOrderRows(SalesOrderJson body, CancellationToken cancellationToken)
	{
		var beersAvailable = new List<SalesOrderRowJson>();
		var availabilities = await availabilityQueries.GetByFilterAsync(null, 0, 10, cancellationToken);
		
		foreach (var row in body.Rows)
		{
			var beerAvailability = availabilities.Results.FirstOrDefault(b => b.BeerId.Equals(row.BeerId.ToString()));
			if (beerAvailability != null && beerAvailability.Quantity.Value >= row.Quantity.Value)
				beersAvailable.Add(row);
		}

		return beersAvailable;
	}

	public async Task<PagedResult<SalesOrderJson>> GetOrdersAsync(CancellationToken cancellationToken)
	{
		var salesOrders = await orderQueries.GetByFilterAsync(null, 0, 100, cancellationToken);

		return salesOrders.TotalRecords > 0
			? new PagedResult<SalesOrderJson>(salesOrders.Results.Select(r => r.ToJson()), salesOrders.Page, salesOrders.PageSize, salesOrders.TotalRecords)
			: new PagedResult<SalesOrderJson>(Enumerable.Empty<SalesOrderJson>(), 0, 0, 0);
	}
}