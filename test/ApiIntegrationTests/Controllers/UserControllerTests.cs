using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ApiIntegrationTests.Fixture;
using Xunit;

namespace ApiIntegrationTests.Controllers;

public class UserControllerTests : IClassFixture<ApiFixture>
{
    private readonly HttpClient _client;
    private readonly ApiFixture _fixture;
    
    public UserControllerTests(ApiFixture fixture)
    {
        _client = fixture.AuthorizedClient;
        _fixture = fixture;
    }
    
    [Fact]
    public async Task GetDisplayName_ShouldReturnDisplayName_WhenAuthorized()
    {
        // Arrange
        // Act
        var result = await _client.GetAsync("api/user/displayName");

        // Assert
        result.EnsureSuccessStatusCode();
        var displayName = await result.Content.ReadAsStringAsync();
        Assert.Equal(ApiFixture.DefaultUser, displayName);
    }
    
    [Fact]
    public async Task GetDisplayName_ShouldReturn401_WhenUnauthorized()
    {
        // Arrange
        var publicClient = _fixture.PublicClient;
        
        // Act
        var result = await publicClient.GetAsync("api/user/displayName");

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
    }
}