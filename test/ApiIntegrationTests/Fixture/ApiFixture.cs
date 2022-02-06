using System.Net.Http;
using System.Net.Http.Headers;
using ApiIntegrationTests.Helpers;
using Opravilo.API;
using Opravilo.API.Models.Requests;
using Opravilo.API.Models.Responses.Account;

namespace ApiIntegrationTests.Fixture;

public class ApiFixture
{
    public static string DefaultUser = "Admin";
    public static string DefaultPassword = "Admin";

    public static int DefaultProjectId = 1;
    public static string DefaultProject = "TestProject";
    public static string DefaultState = "TestState";
    public static int DefaultStateId = 1;
    
    public ApiFixture()
    {
        Factory = new CustomWebApplicationFactory<Startup>();
    }
    
    public HttpClient PublicClient => Factory.CreateClient();

    private HttpClient? _authorizedClient;
    public HttpClient AuthorizedClient
    {
        get
        {
            if (_authorizedClient != null)
            {
                return _authorizedClient;
            }
            
            _authorizedClient = Factory.CreateClient();
            var request = new LoginRequest()
            {
                Login = DefaultUser,
                Password = DefaultPassword,
            };
            var response = _authorizedClient.PostJsonFormAsync("/api/account/login", request).Result;
            var authResult = response.DeserializeResponseAsync<AuthenticationResponse>().Result;
            _authorizedClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResult.Token);
            return _authorizedClient;
        }
    }
    
    private CustomWebApplicationFactory<Startup> Factory { get; }
}