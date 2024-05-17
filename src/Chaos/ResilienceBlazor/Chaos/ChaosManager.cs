using Polly;

namespace ResilienceBlazor.Chaos;

public sealed class ChaosManager() : IChaosManager
{
	public ValueTask<bool> IsChaosEnabledAsync(ResilienceContext context)
	{
		// You con implement your own logic here to enable chaos for specific users or in specific environments
		// Enable chaos 
		return ValueTask.FromResult(true);
	}

	public ValueTask<double> GetInjectionRateAsync(ResilienceContext context)
	{
		throw new NotImplementedException();
	}
}