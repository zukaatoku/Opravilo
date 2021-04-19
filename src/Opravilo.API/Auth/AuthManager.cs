using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Opravilo.API.Models.Responses;
using Opravilo.Application.Interfaces.Services;

namespace Opravilo.API.Auth
{
    public class AuthManager : IAuthManager
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasher _hasher;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly TokenValidationParameters _validationParameters;
        
        public AuthManager(IUserService userService, IPasswordHasher hasher, ITokenGenerator tokenGenerator, 
            TokenValidationParameters validationParameters)
        {
            _userService = userService;
            _hasher = hasher;
            _tokenGenerator = tokenGenerator;
            _validationParameters = validationParameters;
        }
        
        public AuthenticationResult Register(string login, string password)
        {
            var hashedPassword = _hasher.HashPassword(password);
            
            var user = _userService.RegisterUser(login, hashedPassword);

            if (user == null)
            {
                return new AuthenticationResult()
                {
                    IsSuccess = false,
                    Errors = new List<string>()
                    {
                        "Failed to register user!"
                    }
                };
            }

            return Authenticate(user.Login);
        }

        public AuthenticationResult Authenticate(string login, string password)
        {
            var hashedPassword = _hasher.HashPassword(password);

            var user = _userService.FindUser(login, hashedPassword);

            if (user == null)
            {
                return new AuthenticationResult()
                {
                    IsSuccess = false,
                    Errors = new List<string>()
                    {
                        "Failed to find user!"
                    }
                };
            }

            return Authenticate(user.Login);
        }

        public AuthenticationResult RefreshToken(string jwtToken, string refreshToken)
        {
            var clonedParameters = _validationParameters.Clone();
            clonedParameters.ValidateLifetime = false;

            var principal =
                new JwtSecurityTokenHandler().ValidateToken(jwtToken, clonedParameters, out var validatedToken);
            
            var expiredClaim = Convert.ToInt32(principal.Claims.First(c => c.Type == "exp").Value);

            var expirationTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            expirationTime = expirationTime.AddSeconds(expiredClaim);

            var now = DateTime.UtcNow;

            if (expirationTime.CompareTo(now) > 0)
            {
                return new AuthenticationResult()
                {
                    IsSuccess = false,
                    Errors = new List<string>()
                    {
                        "JWT token not expired yet!"
                    }
                };
            }
            
            var loginClaim = principal.Claims.First(c => c.Type == ClaimTypes.Name).Value;

            // todo: check refresh token equals
            return Authenticate(loginClaim);
        }

        private AuthenticationResult Authenticate(string login)
        {
            var token = _tokenGenerator.GetToken(login);
            var refreshToken = _tokenGenerator.GetRefreshToken();

            return new AuthenticationResult()
            {
                IsSuccess = true,
                RefreshToken = refreshToken,
                Token = token
            };
        }
    }
}