namespace ResilienceBlazor.Modules.Sales.Extensions.Dtos;

public record SalesOrderJson(string SalesOrderId, string SalesOrderNumber, Guid CustomerId, string CustomerName, DateTime OrderDate,
	PaymentDetailsJson PaymentDetails, DeliveryAddressJson DeliveryAddress, IEnumerable<SalesOrderRowJson> Rows);
