using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Opravilo.API.Options;

namespace ApiUnitTests.Helpers
{
    public static class FakeTokenGenerator
    {
        public static string GetJwtToken(JwtAuthOptions authOptions, DateTime date, string login, long userId)
        {
            var identity = GetIdentity(login, userId);
            
            var jwt = new JwtSecurityToken(
                notBefore: date,
                claims: identity.Claims,
                expires: date.AddMinutes(authOptions.Lifetime),
                audience: authOptions.Audience,
                issuer: authOptions.Issuer,
                signingCredentials:
                new SigningCredentials(authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }
        
        private static ClaimsIdentity GetIdentity(string userName, long userId)
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