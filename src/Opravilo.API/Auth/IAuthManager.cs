using Opravilo.API.Models.Responses;

namespace Opravilo.API.Auth
{
    public interface IAuthManager
    {
        AuthenticationResult Register(string login, string password);
        AuthenticationResult Authenticate(string login, string password);
        AuthenticationResult RefreshToken(string jwtToken, string refreshToken);
    }
}