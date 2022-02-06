using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ApiIntegrationTests.Fixture;
using ApiIntegrationTests.Helpers;
using Opravilo.API.Models.Responses.Project;
using Opravilo.Application.Models.Requests;
using Xunit;

namespace ApiIntegrationTests.Controllers;

public class ProjectControllerTests : IClassFixture<ApiFixture>
{
    private readonly HttpClient _client;

    public ProjectControllerTests(ApiFixture fixture)
    {
        _client = fixture.AuthorizedClient;
    }

    [Fact]
    public async Task GetProjects_ShouldReturn200()
    {
        // Arrange

        // Act
        var result = await _client.GetAsync("/api/projects/");

        // Assert
        result.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task CreateProject_ShouldReturn200_WhenProjectValid()
    {
        // Arrange
        const string expectedDescription = "Test Project Description";
        const string expectedName = "Test Project Name";

        var request = new CreateProjectRequest()
        {
            Name = expectedName,
            Description = expectedDescription
        };

        // Act
        var result = await CreateProject(request);

        // Assert
        result.EnsureSuccessStatusCode();
        var project = await result.DeserializeResponseAsync<ProjectResponse>();
        Assert.Equal(expectedName, project.Name);
        Assert.Equal(expectedDescription, project.Description);
        Assert.NotEqual(0, project.Id);
    }

    [Fact]
    public async Task GetProject_ShouldReturn200WithDefaultProjectData()
    {
        // Arrange
        const long defaultProjectId = 1;

        // Act
        var result = await _client.GetAsync($"/api/projects/{defaultProjectId}");

        // Assert
        result.EnsureSuccessStatusCode();
        var project = await result.DeserializeResponseAsync<FullProjectResponse>();
        Assert.Equal(ApiFixture.DefaultProject, project.Name);
        Assert.Equal(ApiFixture.DefaultProject, project.Description);
        Assert.NotEmpty(project.States);
    }

    [Fact]
    public async Task DeleteProject_ShouldReturn200AndRemoveProject()
    {
        // Arrange
        const string expectedDescription = "Test Project Description";
        const string expectedName = "Test Project Name";

        var request = new CreateProjectRequest()
        {
            Name = expectedName,
            Description = expectedDescription
        };
        var project = await (await CreateProject(request)).DeserializeResponseAsync<ProjectResponse>();
        
        // Act
        var deleteResult = await _client.DeleteAsync($"api/projects/{project.Id}");
        var getResult = await _client.GetAsync($"api/projects/{project.Id}");

        // Assert
        deleteResult.EnsureSuccessStatusCode();
        // todo: not found?
        Assert.Equal(HttpStatusCode.NoContent, getResult.StatusCode);
    }

    [Fact]
    public async Task UpdateProject_ShouldReturn200AndChangeProject()
    {
        // Arrange
        const string expectedName = "Changed Name";
        const string expectedDescription = "Changed Description";

        var request = new UpdateProjectRequest()
        {
            ProjectId = ApiFixture.DefaultProjectId,
            Name = expectedName,
            Description = expectedDescription,
        };

        // Act
        var updateResult = await _client.PatchJsonFormAsync($"/api/projects/{ApiFixture.DefaultProjectId}", request);
        var getResult = await _client.GetAsync($"api/projects/{ApiFixture.DefaultProjectId}");

        // Assert
        updateResult.EnsureSuccessStatusCode();
        getResult.EnsureSuccessStatusCode();
        var project = await getResult.DeserializeResponseAsync<FullProjectResponse>();
        Assert.Equal(expectedName, project.Name);
        Assert.Equal(expectedDescription, project.Description);
    }

    private async Task<HttpResponseMessage> CreateProject(CreateProjectRequest request)
    {
        return await _client.PostJsonFormAsync("/api/projects/", request);
    }
}