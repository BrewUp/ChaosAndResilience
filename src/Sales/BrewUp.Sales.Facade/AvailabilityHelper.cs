using BrewUp.Sales.ReadModel.Dtos;
using BrewUp.Sales.ReadModel.Queries;
using BrewUp.Sales.ReadModel.Services;
using BrewUp.Shared.ReadModel;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Sales.Facade;

public static class AvailabilityHelper
{
    public static IServiceCollection AddAvailability(this IServiceCollection services)
    {
        services.AddScoped<IBeersService, BeersService>();
        services.AddScoped<IAvailabilityService, AvailabilityService>();
        services.AddScoped<IQueries<Availability>, AvailabilityQueries>();
        services.AddScoped<IQueries<Beers>, BeersQueries>();

        return services;
    }
}