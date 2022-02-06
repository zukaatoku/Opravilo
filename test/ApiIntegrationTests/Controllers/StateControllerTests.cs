using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ApiIntegrationTests.Fixture;
using ApiIntegrationTests.Helpers;
using Opravilo.API.Models.Requests;
using Opravilo.API.Models.Responses.Project;
using Opravilo.API.Models.Responses.State;
using Xunit;

namespace ApiIntegrationTests.Controllers;

public class StateControllerTests : IClassFixture<ApiFixture>
{
    private readonly HttpClient _client;
    
    public StateControllerTests(ApiFixture fixture)
    {
        _client = fixture.AuthorizedClient;
    }

    [Fact]
    public async Task CreateState_ShouldReturn200AndNewState()
    {
        // Arrange
        const string name = "New State";
        var request = new CreateStateRequest()
        {
            Name = name
        };

        // Act
        var response = await CreateState(request);

        // Assert
        response.EnsureSuccessStatusCode();
        var stateResponse = await response.DeserializeResponseAsync<StateResponse>();
        Assert.Equal(name, stateResponse.Name);
        Assert.NotEqual(0, stateResponse.Id);
    }

    [Fact]
    public async Task UpdateState_ShouldReturn200AndChangedState()
    {
        // Arrange
        const string newName = "Updated State Name";
        var request = new UpdateStateRequest()
        {
            Name = newName
        };

        // Act
        var response =
            await _client.PatchJsonFormAsync(
                $"/api/projects/{ApiFixture.DefaultProjectId}/states/{ApiFixture.DefaultStateId}", request);
        
        // Assert
        response.EnsureSuccessStatusCode();
        var stateResponse = await response.DeserializeResponseAsync<StateResponse>();
        Assert.Equal(newName, stateResponse.Name);
        Assert.Equal(ApiFixture.DefaultStateId, stateResponse.Id);
    }

    [Fact]
    public async Task RemoveState_ShouldReturn200AndRemoveState()
    {
        // Arrange
        const string name = "New State";
        var request = new CreateStateRequest()
        {
            Name = name
        };
        var state = await (await CreateState(request)).DeserializeResponseAsync<StateResponse>();
        
        // Act
        var deleteResult = await _client.DeleteAsync($"/api/projects/{ApiFixture.DefaultProjectId}/states/{state.Id}");
        var getResult = await _client.GetAsync($"api/projects/{ApiFixture.DefaultProjectId}");
            
        // Assert
        deleteResult.EnsureSuccessStatusCode();
        var project = await getResult.DeserializeResponseAsync<FullProjectResponse>();
        Assert.DoesNotContain(project.States, s => s.Name == name);
    }

    private async Task<HttpResponseMessage> CreateState(CreateStateRequest request)
    {
        return await _client.PostJsonFormAsync($"/api/projects/{ApiFixture.DefaultProjectId}/states", request);
    }
}