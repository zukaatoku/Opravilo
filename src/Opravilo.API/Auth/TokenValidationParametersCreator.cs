using System;
using Microsoft.IdentityModel.Tokens;
using Opravilo.API.Options;

namespace Opravilo.API.Auth
{
    public class TokenValidationParametersCreator : ITokenValidationParametersCreator
    {
        public TokenValidationParameters Create(JwtAuthOptions options, bool shouldCheckLifetime)
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = options.Issuer,

                ValidateAudience = true,
                ValidAudience = options.Audience,

                ValidateLifetime = shouldCheckLifetime,
                ClockSkew = TimeSpan.Zero,
                
                IssuerSigningKey = options.GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true
            };
        }
    }
}