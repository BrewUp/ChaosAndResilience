using ResilienceBlazor.Shared.CustomTypes;

namespace ResilienceBlazor.Modules.Sales.Extensions.Dtos;

public record BeerJson(string BeerId, string BeerName, Availability Availability);