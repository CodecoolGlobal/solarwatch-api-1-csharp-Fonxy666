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
    private readonly HttpClient _httpClient;
    private readonly Mock<IAuthService> _authServiceMock;

    public AuthenticationControllerIntegrationTests(CustomWebApplicationFactory factory)
    {
        _httpClient = factory.CreateClient();
        _authServiceMock = new Mock<IAuthService>();
        ConfigureAuthService(factory);
    }

    private void ConfigureAuthService(CustomWebApplicationFactory factory)
    {
        using var scope = factory.Services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var testUser = new IdentityUser { UserName = "adminGod", Email = "adminGod@adminGod.com" };
        userManager.CreateAsync(testUser, "asdASDasd123666").GetAwaiter().GetResult();
        
        _authServiceMock.Setup(x => x.LoginAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new AuthResult(true, "adminGod@adminGod.com", "adminGod",
                CreateToken(factory, testUser, "Admin")));
    }

    private string CreateToken(CustomWebApplicationFactory factory, IdentityUser user, string role)
    {
        var tokenService = new TokenService(factory.Services.GetRequiredService<IConfiguration>());
        return tokenService.CreateToken(user, role, false);
    }

    [Fact]
    public async Task Authenticate_Returns_OkResult_With_Valid_Credentials()
    {
        var request = new AuthRequest("Admin", "asdASDasd123666");
        var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        
        var response = await _httpClient.PostAsync("http://localhost:8080/Auth/Login", content);
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var authResponse = await DeserializeResponse<AuthResponse>(response);
        Assert.NotNull(authResponse);
    }

    [Fact]
    public async Task CityPost_Returns_OkResult_With_Valid_Credentials()
    {
        var authResult = await _authServiceMock.Object.LoginAsync("adminGod", "asdASDasd123666");
        ConfigureHttpClientAuthorization(authResult);

        var city = "Algyo";
        var content = new StringContent(JsonSerializer.Serialize(city), Encoding.UTF8, "application/json");
        
        var response = await _httpClient.PostAsync($"http://localhost:8080/CrudAdmin/Post?name={city}", content);
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var authResponse = await DeserializeResponse<AuthResponse>(response);
        Assert.NotNull(authResponse);
    }

    [Fact]
    public async Task CityDelete_Returns_NoContent_With_Valid_Credentials()
    {
        var authResult = await _authServiceMock.Object.LoginAsync("adminGod", "asdASDasd123666");
        ConfigureHttpClientAuthorization(authResult);

        var id = 1;
        
        var response = await _httpClient.DeleteAsync($"http://localhost:8080/CrudAdmin/Delete?id={id}");
        
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    private void ConfigureHttpClientAuthorization(AuthResult authResult)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResult.Token);
    }

    private async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
    {
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}

