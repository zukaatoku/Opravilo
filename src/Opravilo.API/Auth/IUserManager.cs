using Opravilo.API.Models.Responses;

namespace Opravilo.API.Auth
{
    public interface IUserManager
    {
        AuthenticationResult Register(string login, string displayName, string hashedPassword);
        AuthenticationResult Authenticate(string login, string hashedPassword);
        AuthenticationResult AuthenticateOrCreate(string vkId, string givenName, string surname);
        AuthenticationResult RefreshToken(string jwtToken, string refreshToken);
    }
}