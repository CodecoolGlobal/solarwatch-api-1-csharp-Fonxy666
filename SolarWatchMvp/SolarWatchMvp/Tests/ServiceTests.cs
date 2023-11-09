using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using SolarWatchMvp.Services.Authentication;
using Xunit;

namespace SolarWatchMvp.Tests;

public class AuthenticationControllerIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
{
    private readonly WebApplicationFactory<Startup> _factory;

    public AuthenticationControllerIntegrationTests(WebApplicationFactory<Startup> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Authenticate_Returns_OkResult_With_Valid_Credentials()
    {
        var client = _factory.CreateClient();
        var request = new AuthRequest("adminGod", "asdASDasd123666");
        var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        
        var response = await client.PostAsync("http://localhost:8080/Auth/Login", content);
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        var authResponse = JsonSerializer.Deserialize<AuthResponse>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        
        Assert.NotNull(authResponse);
    }
}
