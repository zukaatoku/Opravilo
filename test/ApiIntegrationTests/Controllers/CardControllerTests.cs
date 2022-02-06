using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ApiIntegrationTests.Fixture;
using ApiIntegrationTests.Helpers;
using Opravilo.API.Models.Requests;
using Opravilo.API.Models.Responses.Card;
using Opravilo.API.Models.Responses.State;
using Xunit;

namespace ApiIntegrationTests.Controllers;

public class CardControllerTests : IClassFixture<ApiFixture>
{
    private readonly HttpClient _client;
    private readonly ApiFixture _fixture;
    
    public CardControllerTests(ApiFixture fixture)
    {
        _client = fixture.AuthorizedClient;
        _fixture = fixture;
    }

    [Fact]
    public async Task CreateCard_ShouldReturn200AndNewCard()
    {
        // Arrange
        const string expectedName = "New Card";
        const string expectedDescription = "New Description";
        var request = new CreateCardRequest()
        {
            Name = expectedName,
            Description = expectedDescription
        };

        // Act
        var response = await CreateCard(request);

        // Assert
        response.EnsureSuccessStatusCode();
        var cardResponse = await response.DeserializeResponseAsync<CardResponse>();
        Assert.Equal(expectedName, cardResponse.Name);
        Assert.Equal(expectedDescription, cardResponse.Description);

        var defaultProject = await _fixture.FetchDefaultProject();
        Assert.Contains(defaultProject.States[0].Cards, c => c.Name == expectedName);
    }

    [Fact]
    public async Task UpdateCard_ShouldReturn200AndUpdatedCard()
    {
        // Arrange
        const string expectedName = "Updated Name";
        const string expectedDescription = "Updated Description";
        var request = new UpdateCardRequest()
        {
            Name = expectedName,
            Description = expectedDescription
        };

        // Act
        var response =
            await _client.PatchJsonFormAsync(
                $"/api/projects/{ApiFixture.DefaultProjectId}/cards/{ApiFixture.DefaultCardId}", request);

        // Assert
        response.EnsureSuccessStatusCode();
        var cardResponse = await response.DeserializeResponseAsync<CardResponse>();
        Assert.Equal(expectedName, cardResponse.Name);
        Assert.Equal(expectedDescription, cardResponse.Description);
        
        var defaultProject = await _fixture.FetchDefaultProject();
        Assert.Contains(defaultProject.States[0].Cards, c => c.Name == expectedName && c.Description == expectedDescription);
    }

    [Fact]
    public async Task ChangeState_ShouldReturn200AndChangeState()
    {
        // Arrange
        const string expectedCard = "ChangeStateCard";
        var card = await (await CreateCard(new CreateCardRequest()
            { Name = expectedCard, Description = expectedCard })).DeserializeResponseAsync<CardResponse>();
        var state = await (await CreateState(new CreateStateRequest()
            { Name = "ChangeStateState" })).DeserializeResponseAsync<StateResponse>();

        // Act
        var response =
            await _client.PatchAsync($"/api/projects/{ApiFixture.DefaultProjectId}/cards/{card.Id}/state/{state.Id}",
                null);

        // Assert
        response.EnsureSuccessStatusCode();
        var project = await _fixture.FetchDefaultProject();
        var changedStateCard = project.States.First(s => s.Id == state.Id).Cards.First(c => c.Id == card.Id);
        Assert.Equal(expectedCard, changedStateCard.Name);
        Assert.DoesNotContain(project.States[0].Cards, c => c.Id == card.Id);
    }
    
    [Fact]
    public async Task RemoveCard_ShouldReturn200AndRemoveCard()
    {
        // Arrange
        const string expectedCard = "Card To Remove";
        var request = new CreateCardRequest()
        {
            Name = expectedCard,
            Description = expectedCard
        };
        var card = await (await CreateCard(request)).DeserializeResponseAsync<CardResponse>();

        // Act
        var response = await _client.DeleteAsync($"/api/projects/{ApiFixture.DefaultProjectId}/cards/{card.Id}");

        // Assert
        response.EnsureSuccessStatusCode();

        var defaultProject = await _fixture.FetchDefaultProject();
        Assert.DoesNotContain(defaultProject.States[0].Cards, c => c.Name == expectedCard);
    }

    private async Task<HttpResponseMessage> CreateCard(CreateCardRequest request)
    {
        return await _client.PostJsonFormAsync(
            $"/api/projects/{ApiFixture.DefaultProjectId}/states/{ApiFixture.DefaultStateId}/cards", request);
    }
    
    private async Task<HttpResponseMessage> CreateState(CreateStateRequest request)
    {
        return await _client.PostJsonFormAsync($"/api/projects/{ApiFixture.DefaultProjectId}/states", request);
    }
}