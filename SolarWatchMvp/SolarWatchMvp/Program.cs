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
builder.Services.AddSingleton<ICityRepository, CityRepository>();
builder.Services.AddSingleton<ISunTimeRepository, SunTimeRepository>();

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
    using var db = new WeatherApiContext();
    PrintData();
    return;

    void PrintData()
    {
        foreach (var city in db.Cities)
        {
            Console.WriteLine($"Id: {city.Id}, Name: {city.Name},  Coordinate id: {city.CoordinateId}, State: {city.State}, Country: {city.Country}");
        }
        foreach (var dbSunTime in db.SunTimes)
        {
            Console.WriteLine($"Id: {dbSunTime.Id}, Sunrise time: {dbSunTime.SunRiseTime}, Sunset time: {dbSunTime.SunSetTime}");
        }
    }
}

InitializeDb();

app.Run();