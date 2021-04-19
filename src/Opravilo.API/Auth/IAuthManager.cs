using Opravilo.API.Models.Responses;

namespace Opravilo.API.Auth
{
    public interface IAuthManager
    {
        AuthenticationResult Register(string login, string password);
    }
}