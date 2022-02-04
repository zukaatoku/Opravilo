using Opravilo.API;

namespace ApiIntegrationTests.Fixture;

public class ApiFixture
{
    public static string DefaultUser = "Admin";
    public static string DefaultPassword = "Admin";
    
    public ApiFixture()
    {
        Factory = new CustomWebApplicationFactory<Startup>();
    }
    
    public CustomWebApplicationFactory<Startup> Factory { get; }
}