using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Opravilo.API.Options;

namespace Opravilo.API.Auth
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly AuthOptions _authOptions;

        public TokenGenerator(AuthOptions authOptions)
        {
            _authOptions = authOptions;
        }

        public string GetToken(string login)
        {
            var identity = GetIdentity(login);
            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                notBefore: now,
                claims: identity.Claims,
                expires: now.AddMinutes(_authOptions.Lifetime),
                audience: _authOptions.Audience,
                issuer: _authOptions.Issuer,
                signingCredentials:
                new SigningCredentials(_authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
        
        private ClaimsIdentity GetIdentity(string userName)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };

            var identity = new ClaimsIdentity(claims, "Token", 
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            return identity;
        }
    }
}