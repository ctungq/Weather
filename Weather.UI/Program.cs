using Weather.UI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string apiBaseUrl = builder.Configuration.GetValue<string>("WeatherApiBaseUrl");
string apiApiQuery = builder.Configuration.GetValue<string>("WeatherApiQuery");
string apiApiKey = builder.Configuration.GetValue<string>("WeatherApi_ApiKey");

builder.Services.AddScoped<IWeatherApiService>(svc => new WeatherApiService(apiApiKey, apiBaseUrl, apiApiQuery));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
