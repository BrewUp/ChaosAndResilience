using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.CircuitBreaker;
using Polly.Fallback;
using Polly.Retry;
using ResilienceBlazor.Modules.Sales.Extensions.Dtos;
using ResilienceBlazor.Shared.Configuration;

namespace ResilienceBlazor.Modules.Sales.Extensions;

public static class SalesHelper
{
	public static IServiceCollection AddResilienceSalesModule(this IServiceCollection services, AppConfiguration configuration)
	{
		services.AddScoped<ISalesService, SalesService>();

		services.AddGetSalesOrderResiliencePipeline();
		services.AddPostSalesOrderResiliencePipeline();

		return services;
	}

	public static IServiceCollection AddGetSalesOrderResiliencePipeline(this IServiceCollection services)
	{
		services.AddResiliencePipeline<string, PagedResult<SalesOrderJson>>("brewup-getsalesorders-resilience",
			resiliencePipelineBuilder =>
			{
				resiliencePipelineBuilder
					.AddRetry(new RetryStrategyOptions<PagedResult<SalesOrderJson>>
					{
						Delay = TimeSpan.FromMilliseconds(200),
						BackoffType = DelayBackoffType.Exponential,
						MaxRetryAttempts = 3,
						ShouldHandle = new PredicateBuilder<PagedResult<SalesOrderJson>>()
							.Handle<ApplicationException>(),
						OnRetry = retryArguments =>
						{
							Console.WriteLine($"Current Attempt: {retryArguments.AttemptNumber}");

							return ValueTask.CompletedTask;
						}
					})
					.AddFallback(new FallbackStrategyOptions<PagedResult<SalesOrderJson>>
					{
						FallbackAction = _ =>
							Outcome.FromResultAsValueTask(
								new PagedResult<SalesOrderJson>(Enumerable.Empty<SalesOrderJson>(), 1, 0, 0))
					})
					.AddCircuitBreaker(new CircuitBreakerStrategyOptions<PagedResult<SalesOrderJson>>
					{
						FailureRatio = 0.5,  // Default value 0.1 => 10%
						MinimumThroughput = 90,  // Default value 100
						SamplingDuration = TimeSpan.FromSeconds(25),  // Default value 30 seconds
						BreakDuration = TimeSpan.FromSeconds(5)
					});
			});

		return services;
	}

	public static IServiceCollection AddPostSalesOrderResiliencePipeline(this IServiceCollection services)
	{
		services.AddResiliencePipeline<string, string>("brewup-postsalesorders-resilience",
			resiliencePipelineBuilder =>
			{
				resiliencePipelineBuilder
					.AddRetry(new RetryStrategyOptions<string>
					{
						Delay = TimeSpan.FromMilliseconds(200),
						MaxRetryAttempts = 3
					})
					.AddFallback(new FallbackStrategyOptions<string>
					{
						FallbackAction = _ =>
							Outcome.FromResultAsValueTask(
								string.Empty)
					})
					.AddCircuitBreaker(new CircuitBreakerStrategyOptions<string>
					{
						FailureRatio = 0.5,  // Default value 0.1 => 10%
						MinimumThroughput = 90,  // Default value 100
						SamplingDuration = TimeSpan.FromSeconds(25),  // Default value 30 seconds
						BreakDuration = TimeSpan.FromSeconds(5)
					});
			});

		return services;
	}
}