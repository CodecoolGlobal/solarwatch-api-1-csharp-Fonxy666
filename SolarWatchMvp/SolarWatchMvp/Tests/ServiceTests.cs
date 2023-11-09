using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using SolarWatchMvp.Data;
using SolarWatchMvp.Services.Authentication;
using Xunit;

namespace SolarWatchMvp.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<WeatherApiContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<WeatherApiContext>(options =>
                options.UseInMemoryDatabase("TestDb"));
        });
    }
}

public class AuthenticationControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _httpClient;

    public AuthenticationControllerIntegrationTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _httpClient = _factory.CreateClient();
    }

    [Fact]
    public async Task Authenticate_Returns_OkResult_With_Valid_Credentials()
    {
        var request = new AuthRequest("adminGod", "asdASDasd123666");
        var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("http://localhost:8080/Auth/Login", content);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        var authResponse = JsonSerializer.Deserialize<AuthResponse>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.NotNull(authResponse);
    }

    [Fact]
    public async Task CityGet_Returns_OkResult_With_Valid_Credentials()
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJUb2tlbkZvclRoZUFwaVdpdGhBdXRoIiwianRpIjoiY2JhMDkwZjgtOGYxYi00ZDJlLWI3NDQtNWIxYTMxMGYwOTYyIiwiaWF0IjoiMTEvMDkvMjAyMyAyMzo0NjowOCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiYTFiNGNhMzYtZDZmZC00OGVlLWIwZTEtMjVhMTliNmNkZTQ4IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6ImFkbWluR29kIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoiYWRtaW5Hb2RAd2luZG93c2xpdmUuY29tIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW4iLCJleHAiOjE2OTk1NzUzNjgsImlzcyI6ImFwaVdpdGhBdXRoQmFja2VuZCIsImF1ZCI6ImFwaVdpdGhBdXRoQmFja2VuZCJ9.kFrCJ0rdNXd0ZMOqCw7WiQSIK2kXkZC0piXH3MrzgZk");
        var city = "Debrecen";
        var content = new StringContent(JsonSerializer.Serialize(city), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"http://localhost:8080/CrudAdmin/Post?name={city}", content);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        var authResponse = JsonSerializer.Deserialize<AuthResponse>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.NotNull(authResponse);
    }
    
    [Fact]
    public async Task CityDelete_Returns_OkResult_With_Valid_Credentials()
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJUb2tlbkZvclRoZUFwaVdpdGhBdXRoIiwianRpIjoiY2JhMDkwZjgtOGYxYi00ZDJlLWI3NDQtNWIxYTMxMGYwOTYyIiwiaWF0IjoiMTEvMDkvMjAyMyAyMzo0NjowOCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiYTFiNGNhMzYtZDZmZC00OGVlLWIwZTEtMjVhMTliNmNkZTQ4IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6ImFkbWluR29kIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoiYWRtaW5Hb2RAd2luZG93c2xpdmUuY29tIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW4iLCJleHAiOjE2OTk1NzUzNjgsImlzcyI6ImFwaVdpdGhBdXRoQmFja2VuZCIsImF1ZCI6ImFwaVdpdGhBdXRoQmFja2VuZCJ9.kFrCJ0rdNXd0ZMOqCw7WiQSIK2kXkZC0piXH3MrzgZk");
        var id = 1;
        var cancellationToken = new CancellationToken();

        var response = await _httpClient.DeleteAsync($"http://localhost:8080/CrudAdmin/Delete?id={id}", cancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        try
        {
            var authResponse = JsonSerializer.Deserialize<AuthResponse>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Assert.NotNull(authResponse);
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error occurred during JSON deserialization: {ex.Message}");
        }
    }
}

