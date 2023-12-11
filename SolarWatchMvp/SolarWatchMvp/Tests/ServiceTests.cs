using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Moq;
using SolarWatchMvp.Data;
using SolarWatchMvp.Services.Authentication;
using Xunit;
using Xunit.Abstractions;

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
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly HttpClient _httpClient;
    private readonly Mock<IAuthService> _authServiceMock;

    public AuthenticationControllerIntegrationTests(CustomWebApplicationFactory factory, ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
        _httpClient = _factory.CreateClient();
        var tokenService = new TokenService();

        using var scope = _factory.Services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var testUser = new IdentityUser { UserName = "adminGod", Email = "adminGod@adminGod.com" };
        userManager.CreateAsync(testUser, "asdASDasd123666").GetAwaiter().GetResult();
        _authServiceMock = new Mock<IAuthService>();
        _authServiceMock.Setup(x => x.LoginAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new AuthResult(true, "adminGod@adminGod.com", "adminGod",
                tokenService.CreateToken(testUser, "Admin", false)));
    }

    [Fact]
    public async Task Authenticate_Returns_OkResult_With_Valid_Credentials()
    {
        var request = new AuthRequest("Admin", "asdASDasd123666");
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
        var authResult = await _authServiceMock.Object.LoginAsync("adminGod", "asdASDasd123666");

        if (authResult.Success)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResult.Token);

            var city = "Algyo";
            var content = new StringContent(JsonSerializer.Serialize(city), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"http://localhost:8080/CrudAdmin/Post?name={city}", content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var authResponse = JsonSerializer.Deserialize<AuthResponse>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.NotNull(authResponse);
        }
    }
    
    [Fact]
    public async Task CityDelete_Returns_OkResult_With_Valid_Credentials()
    {
        var authResult = await _authServiceMock.Object.LoginAsync("adminGod", "asdASDasd123666");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResult.Token);
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

