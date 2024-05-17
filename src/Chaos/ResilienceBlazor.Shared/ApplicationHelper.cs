using Microsoft.Extensions.DependencyInjection;
using ResilienceBlazor.Shared.Abstracts;
using ResilienceBlazor.Shared.Concretes;

namespace ResilienceBlazor.Shared;

public static class ApplicationHelper
{
	public static IServiceCollection AddApplicationService(this IServiceCollection services)
	{
		services.AddHttpClient<IHttpService, HttpService>();

		return services;
	}
}