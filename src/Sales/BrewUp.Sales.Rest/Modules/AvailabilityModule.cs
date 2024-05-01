using BrewUp.Sales.Facade;
using BrewUp.Sales.Facade.Endpoints;

namespace BrewUp.Sales.Rest.Modules;

public class AvailabilityModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 0;
    public IServiceCollection RegisterModule(WebApplicationBuilder builder) => builder.Services.AddAvailability();

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints.MapAvailabilityEndpoints();
}