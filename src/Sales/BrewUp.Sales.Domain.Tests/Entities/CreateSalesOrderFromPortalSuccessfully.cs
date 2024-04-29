using BrewUp.Sales.Domain.CommandHandlers;
using BrewUp.Sales.SharedKernel.Commands;
using BrewUp.Sales.SharedKernel.CustomTypes;
using BrewUp.Sales.SharedKernel.Events;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainIds;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;
using Muflone.SpecificationTests;

namespace BrewUp.Sales.Domain.Tests.Entities;

public sealed class CreateSalesOrderFromPortalSuccessfully : CommandSpecification<CreateSalesOrderFromPortal>
{
	private readonly SalesOrderId _salesOrderId = new(Guid.NewGuid());
	private readonly SalesOrderNumber _salesOrderNumber = new("20240315-1500");
	private readonly OrderDate _orderDate = new(DateTime.UtcNow);

	private readonly Guid _correlationId = Guid.NewGuid();

	private readonly CustomerId _customerId = new(Guid.NewGuid());
	private readonly CustomerName _customerName = new("Muflone");

	private readonly PaymentDetailsJson _paymentDetails =
		new("1234567890123456", DateTime.UtcNow.AddYears(1), "123");
	private readonly DeliveryAddressJson _deliveryAddress =
		new("Home", "1234 Main St", "Springfield", "IL", "62704", "555-555-5555");
	
	private readonly IEnumerable<SalesOrderRowJson> _rows = Enumerable.Empty<SalesOrderRowJson>();

	protected override IEnumerable<DomainEvent> Given()
	{
		yield break;
	}

	protected override CreateSalesOrderFromPortal When()
	{
		return new CreateSalesOrderFromPortal(_salesOrderId, _correlationId, _salesOrderNumber, _orderDate, _customerId,
			_customerName, _paymentDetails, _deliveryAddress, _rows);
	}

	protected override ICommandHandlerAsync<CreateSalesOrderFromPortal> OnHandler()
	{
		return new CreateSalesOrderCommandHandler(Repository, new NullLoggerFactory());
	}

	protected override IEnumerable<DomainEvent> Expect()
	{
		yield return new SalesOrderFromPortalCreated(_salesOrderId, _correlationId, _salesOrderNumber, _orderDate,
			_customerId, _customerName, _paymentDetails, _deliveryAddress, _rows);
	}
}