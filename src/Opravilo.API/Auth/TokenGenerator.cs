using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Opravilo.API.Options;

namespace Opravilo.API.Auth
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly JwtAuthOptions _authOptions;

        public TokenGenerator(JwtAuthOptions authOptions)
        {
            _authOptions = authOptions;
        }

        public string GetToken(string login, long userId)
        {
            var identity = GetIdentity(login, userId);
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

        public string GetRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private ClaimsIdentity GetIdentity(string userName, long userId)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim("sub", userId.ToString())
            };

            var identity = new ClaimsIdentity(claims, "Token", 
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            return identity;
        }
    }
}