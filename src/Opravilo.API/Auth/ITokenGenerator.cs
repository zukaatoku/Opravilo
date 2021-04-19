namespace Opravilo.API.Auth
{
    public interface ITokenGenerator
    {
        string GetToken(string login, long userId);
        string GetRefreshToken();
    }
}