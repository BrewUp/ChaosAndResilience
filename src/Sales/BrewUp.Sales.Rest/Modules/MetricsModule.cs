using OpenTelemetry.Metrics;

namespace BrewUp.Sales.Rest.Modules;

public class MetricsModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 0;

	public IServiceCollection RegisterModule(WebApplicationBuilder builder)
	{
		builder.Services.AddOpenTelemetry()
			// Add Metrics for ASP.NET Core and our custom metrics and export to Prometheus
			.WithMetrics(meterProviderBuilder =>
			{
				// Metrics provider from OpenTelemetry
				meterProviderBuilder.AddAspNetCoreInstrumentation();
				meterProviderBuilder.AddAspNetCoreInstrumentation();
				meterProviderBuilder.AddMeter();
				// Metrics provides by ASP.NET Core in .NET 8
				meterProviderBuilder.AddMeter("Microsoft.AspNetCore.Hosting");
				meterProviderBuilder.AddMeter("Microsoft.AspNetCore.Server.Kestrel");
				meterProviderBuilder.AddPrometheusExporter();
			});
		
		return builder.Services;
	}

	public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
	{
		endpoints.MapPrometheusScrapingEndpoint();
		
		return endpoints;
	}
}