using AspNetCoreRateLimit;
using Weather.Api;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddScoped<IApiKeyValidator, ApiKeyValidator>();
        builder.Services.AddScoped<IWeatherService, WeatherService>();
        builder.Services.AddScoped<IWeatherMapGateway, WeatherMapGateway>();

        //rate limit for each api key
        builder.Services.AddOptions();
        builder.Services.AddMemoryCache();
        builder.Services.Configure<ClientRateLimitOptions>(builder.Configuration.GetSection("ClientRateLimiting"));
        builder.Services.AddSingleton<IClientPolicyStore, MemoryCacheClientPolicyStore>();
        builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
        builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
        builder.Services.AddInMemoryRateLimiting();
        
        var app = builder.Build();
        {
            app.UseExceptionHandler("/error");
            app.Use(async (context, next) =>
            {
                // Get the API key from the X-API-KEY header
                var apiKey = context.Request.Headers["X-API-KEY"];

                // Validate the API key using the IApiKeyValidator service
                var apiKeyValidator = context.RequestServices.GetRequiredService<IApiKeyValidator>();
                if (!apiKeyValidator.Validate(apiKey))
                {
                    // If the API key is invalid, return a 401 Unauthorized response
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }

                // If the API key is valid, pass the request to the next middleware in the pipeline
                await next.Invoke();
            });
            app.UseClientRateLimiting();
            app.UseHttpsRedirection();
            app.MapControllers();
            app.Run();
        }
    }
}