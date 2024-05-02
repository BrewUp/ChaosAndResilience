using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainIds;
using Muflone.Messages.Events;

namespace BrewUp.Warehouses.SharedKernel.Events;

public sealed class SalesOrderConfirmed(SalesOrderId aggregateId, Guid commitId, SalesOrderNumber salesOrderNumber,
    IEnumerable<SalesOrderRowJson> rows) : IntegrationEvent(aggregateId, commitId)
{
    public readonly SalesOrderId SalesOrderId = aggregateId;
    public readonly SalesOrderNumber SalesOrderNumber = salesOrderNumber;

    public readonly IEnumerable<SalesOrderRowJson> Rows = rows;
}