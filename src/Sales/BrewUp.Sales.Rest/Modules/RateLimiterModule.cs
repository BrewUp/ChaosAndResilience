using System.Threading.RateLimiting;
using BrewUp.Shared.Configurations;
using Microsoft.AspNetCore.RateLimiting;

namespace BrewUp.Sales.Rest.Modules;

// https://learn.microsoft.com/en-us/aspnet/core/performance/rate-limit?view=aspnetcore-8.0
public class RateLimiterModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 0;
    
    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        var rateLimitSettings = new RateLimiting();
        builder.Configuration.GetSection("BrewUp:RateLimitSettings")
            .Bind(rateLimitSettings);
        
        builder.Services.Configure<RateLimiting>(
            builder.Configuration.GetSection("BrewUp:RateLimitSettings"));

        // Fixed Window Limiter
        if (rateLimitSettings.FixedWindow.Enabled)
        {
            RateLimiting.CurrentRateLimiter = "fixed";
            builder.Services.AddRateLimiter(rateLimiterOptions => rateLimiterOptions
                .AddFixedWindowLimiter(policyName: "fixed", options =>
                {
                    options.PermitLimit = int.Parse(builder.Configuration["BrewUp:RateLimitSettings:FixedWindow:PermitLimit"]!);
                    options.Window = TimeSpan.FromSeconds(int.Parse(builder.Configuration["BrewUp:RateLimitSettings:FixedWindow:TimeWindowInSeconds"]!));
                    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    options.QueueLimit = int.Parse(builder.Configuration["BrewUp:RateLimitSettings:FixedWindow:QueueLimit"]!);
                }));
        }
        
        // Sliding Window Limiter
        if (rateLimitSettings.SlidingWindow.Enabled)
        {
            RateLimiting.CurrentRateLimiter = "sliding";
            builder.Services.AddRateLimiter(rateLimiterOptions => rateLimiterOptions
                .AddSlidingWindowLimiter(policyName: "sliding", options =>
                {
                    options.PermitLimit = int.Parse(builder.Configuration["BrewUp:RateLimitSettings:SlidingWindow:PermitLimit"]!);
                    options.Window = TimeSpan.FromSeconds(int.Parse(builder.Configuration["BrewUp:RateLimitSettings:SlidingWindow:SecondsPerWindow"]!));
                    options.SegmentsPerWindow = int.Parse(builder.Configuration["BrewUp:RateLimitSettings:SlidingWindow:SegmentsPerWindow"]!);
                    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    options.QueueLimit = int.Parse(builder.Configuration["BrewUp:RateLimitSettings:SlidingWindow:QueueLimit"]!);
                }));
        }
        
        // Token Bucket Limiter
        if (rateLimitSettings.TokenBucket.Enabled)
        {
            RateLimiting.CurrentRateLimiter = "token";
            builder.Services.AddRateLimiter(rateLimiterOptions => rateLimiterOptions
                .AddTokenBucketLimiter(policyName: "token", options =>
                {
                    options.TokenLimit = int.Parse(builder.Configuration["BrewUp:RateLimitSettings:TokenBucket:TokenLimit"]!);
                    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    options.QueueLimit = int.Parse(builder.Configuration["BrewUp:RateLimitSettings:TokenBucket:QueueLimit"]!);
                    options.ReplenishmentPeriod = TimeSpan.FromSeconds(int.Parse(builder.Configuration["BrewUp:RateLimitSettings:TokenBucket:ReplenishmentPeriod"]!));
                    options.TokensPerPeriod = int.Parse(builder.Configuration["BrewUp:RateLimitSettings:TokenBucket:TokensPerPeriod"]!);
                    options.AutoReplenishment = bool.Parse(builder.Configuration["BrewUp:RateLimitSettings:TokenBucket:AutoReplenishment"]!);
                }));
        }
        
        // Concurrency Limiter
        if (rateLimitSettings.Concurrency.Enabled)
        {
            RateLimiting.CurrentRateLimiter = "concurrency";
            builder.Services.AddRateLimiter(rateLimiterOptions => rateLimiterOptions
                .AddConcurrencyLimiter(policyName: "concurrency", options =>
                {
                    options.PermitLimit = int.Parse(builder.Configuration["BrewUp:RateLimitSettings:Concurrency:PermitLimit"]!);
                    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    options.QueueLimit = int.Parse(builder.Configuration["BrewUp:RateLimitSettings:Concurrency:QueueLimit"]!);
                }));
        }

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
}