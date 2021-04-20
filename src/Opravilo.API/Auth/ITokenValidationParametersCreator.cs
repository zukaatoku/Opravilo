using Microsoft.IdentityModel.Tokens;
using Opravilo.API.Options;

namespace Opravilo.API.Auth
{
    public interface ITokenValidationParametersCreator
    {
        TokenValidationParameters Create(AuthOptions options, bool shouldCheckLifetime);
    }
}