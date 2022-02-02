using System.Net.Http;
using System.Threading.Tasks;
using ApiIntegrationTests.Helpers;
using Opravilo.API;
using Opravilo.API.Models.Requests;
using Opravilo.API.Models.Responses.Account;
using Xunit;

namespace ApiIntegrationTests;

public class AccountControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
{
    private readonly HttpClient _client;

    public AccountControllerTests(CustomWebApplicationFactory<Startup> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Login_ShouldReturn200WithErrors_WhenNoUser()
    {
        // Arrange
        var request = new LoginRequest()
        {
            Login = "NotExist",
            Password = "NotExist",
        };

        // Act
        var result = await _client.PostJsonFormAsync("/api/account/login", request);

        // Assert
        result.EnsureSuccessStatusCode();
        var response = await result.DeserializeResponseAsync<AuthenticationResponse>();
        Assert.NotEmpty(response.Errors);
        Assert.Null(response.Token);
        Assert.False(response.IsSuccess);
    }

    [Fact]
    public async Task Register_ShouldReturn200WithCredentialsData_WhenSuccess()
    {
        // Arrange
        var request = new RegistrationRequest()
        {
            DisplayName = "Test",
            Login = "Test",
            Password = "Test"
        };
        
        // Act
        var result = await _client.PostJsonFormAsync("api/account/register", request);
        
        // Assert
        result.EnsureSuccessStatusCode();
        var response = await result.DeserializeResponseAsync<AuthenticationResponse>();
        Assert.Null(response.Errors);
        Assert.NotNull(response.Token);
        Assert.True(response.IsSuccess);
    }
}