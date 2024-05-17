using Polly;

namespace ResilienceBlazor.Chaos;

public interface IChaosManager
{
	ValueTask<bool> IsChaosEnabledAsync(ResilienceContext context);
	ValueTask<double> GetInjectionRateAsync(ResilienceContext context);
}