using Microsoft.IdentityModel.Tokens;
using Opravilo.API.Options;

namespace Opravilo.API.Auth
{
    public interface ITokenValidationParametersCreator
    {
        TokenValidationParameters Create(JwtAuthOptions options, bool shouldCheckLifetime);
    }
}