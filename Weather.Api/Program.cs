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

        
        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        // builder.Services.AddEndpointsApiExplorer();
        // builder.Services.AddSwaggerGen();

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
            app.UseHttpsRedirection();
            app.MapControllers();
            app.Run();
        }
    }
}

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }



// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast")
// .WithOpenApi();



// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
