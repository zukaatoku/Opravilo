namespace Opravilo.API.Auth
{
    public interface ITokenGenerator
    {
        string GetToken(string login);
        string GetRefreshToken();
    }
}