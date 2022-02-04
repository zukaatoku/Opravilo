using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ApiIntegrationTests.Fixture;
using ApiIntegrationTests.Helpers;
using Opravilo.API.Models.Requests;
using Opravilo.API.Models.Responses.Account;
using Xunit;

namespace ApiIntegrationTests;

public class AccountControllerTests : IClassFixture<ApiFixture>
{
    private readonly HttpClient _client;
    private readonly ApiFixture _fixture;

    public AccountControllerTests(ApiFixture fixture)
    {
        _fixture = fixture;
        _client = fixture.Factory.CreateClient();
    }
    
    [Fact]
    public async Task Login_ShouldReturn200_WhenUserExist()
    {
        // Arrange
        var request = new LoginRequest()
        {
            Login = ApiFixture.DefaultUser,
            Password = ApiFixture.DefaultPassword,
        };

        // Act
        var result = await _client.PostJsonFormAsync("/api/account/login", request);

        // Assert
        result.EnsureSuccessStatusCode();
        var response = await result.DeserializeResponseAsync<AuthenticationResponse>();
        Assert.Null(response.Errors);
        Assert.NotNull(response.Token);
        Assert.True(response.IsSuccess);
        // 1) jwt
        // 2) refresh
        // 3) auth state
        Assert.Equal(3, result.Headers.GetValues("Set-Cookie").Count());
    }

    [Fact]
    public async Task Logout_ShouldReturn200()
    {
        // Arrange
        var request = new LoginRequest()
        {
            Login = ApiFixture.DefaultUser,
            Password = ApiFixture.DefaultPassword,
        };

        // Act
        var result = await _client.PostJsonFormAsync("/api/account/login", request);
        result.EnsureSuccessStatusCode();
        var response = await result.DeserializeResponseAsync<AuthenticationResponse>();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.Token);
        var logoutResult = await _client.PostJsonFormAsync("/api/account/logout", null);
        
        // Assert
        logoutResult.EnsureSuccessStatusCode();
        // todo: assert cookies clear
    }

    [Fact]
    public async Task Login_ShouldReturn401WithErrors_WhenNoUser()
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
        Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
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
    
    // todo: rethink status code
    [Fact]
    public async Task Register_ShouldReturn200WithError_WhenTryToRegisterExistingUserSecondTime()
    {
        // Arrange
        var request = new RegistrationRequest()
        {
            DisplayName = ApiFixture.DefaultUser,
            Login = ApiFixture.DefaultUser,
            Password = ApiFixture.DefaultPassword
        };
        
        // Act
        var result = await _client.PostJsonFormAsync("api/account/register", request);
        
        // Assert
        result.EnsureSuccessStatusCode();
        var response = await result.DeserializeResponseAsync<AuthenticationResponse>();
        Assert.NotNull(response.Errors);
        Assert.Null(response.Token);
        Assert.False(response.IsSuccess);
    }
}