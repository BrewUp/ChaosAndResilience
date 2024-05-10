using BrewUp.Sales.Rest.Modules;
using BrewUp.Shared.Configurations;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// Register Modules
builder.RegisterModules();

var app = builder.Build();

app.UseCors("CorsPolicy");

// Register endpoints
app.MapEndpoints();

if (!string.IsNullOrWhiteSpace(RateLimiting.CurrentRateLimiter))
	app.UseRateLimiter();

// Configure the HTTP request pipeline.
app.UseSwagger(s =>
{
	s.RouteTemplate = "documentation/{documentName}/documentation.json";
});
app.UseSwaggerUI(s =>
{
	s.SwaggerEndpoint("/documentation/v1/documentation.json", "BrewUp Sales");
	s.RoutePrefix = "documentation";
});

app.Use(async (context, next) =>
{
	var tagsFeature = context.Features.Get<IHttpMetricsTagsFeature>();
	if (tagsFeature != null)
	{
		var source = context.Request.Query["utm_medium"].ToString() switch
		{
			"" => "none",
			"social" => "social",
			"email" => "email",
			"organic" => "organic",
			_ => "other"
		};
		tagsFeature.Tags.Add(new KeyValuePair<string, object?>("mkt_medium", source));
	}

	await next.Invoke();
});

app.Run();