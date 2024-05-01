﻿using BrewUp.Sales.SharedKernel.CustomTypes;
using BrewUp.Shared.Contracts;
using Muflone.Messages.Events;

namespace BrewUp.Sales.SharedKernel.Events;

public sealed class SalesOrderConfirmed(SalesOrderId aggregateId, Guid commitId, SalesOrderNumber salesOrderNumber,
    IEnumerable<SalesOrderRowJson> rows) : IntegrationEvent(aggregateId, commitId)
{
    public readonly SalesOrderId SalesOrderId = aggregateId;
    public readonly SalesOrderNumber SalesOrderNumber = salesOrderNumber;

    public readonly IEnumerable<SalesOrderRowJson> Rows = rows;
}