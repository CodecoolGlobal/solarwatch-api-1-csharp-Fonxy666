using Microsoft.EntityFrameworkCore;
using SolarWatchMvp.Controllers;
using SolarWatchMvp.Data;
using SolarWatchMvp.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(
    options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IWeatherDataProvider, OpenWeatherMapApi>();
builder.Services.AddSingleton<IJsonProcessor, JsonProcessor>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ISunTimeRepository, SunTimeRepository>();
builder.Services.AddDbContext<WeatherApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

void InitializeDb()
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<WeatherApiContext>();
    PrintData(context);
    return;

    void PrintData(WeatherApiContext functionContext)
    {
        foreach (var city in functionContext.Cities)
        {
            Console.WriteLine($"Id: {city.Id}, Name: {city.Name},  Latitude: {city.Latitude}, Longitude: {city.Longitude}, State: {city.State}, Country: {city.Country}");
        }
        foreach (var dbSunTime in functionContext.SunTimes)
        {
            Console.WriteLine($"Id: {dbSunTime.Id}, Sunrise time: {dbSunTime.SunRiseTime}, Sunset time: {dbSunTime.SunSetTime}");
        }
    }
}

InitializeDb();

app.Run();