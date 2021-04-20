using Opravilo.API.Models.Responses;

namespace Opravilo.API.Auth
{
    public interface IAuthManager
    {
        AuthenticationResult Register(string login, string hashedPassword);
        AuthenticationResult Authenticate(string login, string hashedPassword);
        AuthenticationResult RefreshToken(string jwtToken, string refreshToken);
    }
}