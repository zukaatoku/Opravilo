using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Opravilo.API.Models.Responses;
using Opravilo.API.Options;
using Opravilo.Application.Interfaces.Services;

namespace Opravilo.API.Auth
{
    public class AuthManager : IAuthManager
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasher _hasher;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly TokenValidationParameters _validationParameters;
        private readonly AuthOptions _authOptions;
        
        public AuthManager(IUserService userService, IPasswordHasher hasher, ITokenGenerator tokenGenerator, 
            TokenValidationParameters validationParameters, AuthOptions authOptions)
        {
            _userService = userService;
            _hasher = hasher;
            _tokenGenerator = tokenGenerator;
            _validationParameters = validationParameters;
            _authOptions = authOptions;
        }
        
        public AuthenticationResult Register(string login, string password)
        {
            var hashedPassword = _hasher.HashPassword(password);
            
            var user = _userService.RegisterUser(login, hashedPassword);

            if (user == null)
            {
                return Fail("Failed to register user!");
            }

            return Authenticate(user.Login, user.Id);
        }

        public AuthenticationResult Authenticate(string login, string password)
        {
            var hashedPassword = _hasher.HashPassword(password);

            var user = _userService.FindUser(login, hashedPassword);

            if (user == null)
            {
                return Fail("Failed to find user!");
            }

            return Authenticate(user.Login, user.Id);
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
                return Fail("JWT token not expired yet!");
            }
            
            var loginClaim = principal.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            var idClaim = Convert.ToInt32(principal.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            
            var savedToken = _userService.FindToken(idClaim);
            
            if (savedToken.RefreshToken != refreshToken)
            {
                return Fail("Refresh token invalid!");
            }
            
            if (now.CompareTo(savedToken.ExpirationDate) > 0)
            {
                return Fail("Refresh token expired!");
            }

            return Authenticate(loginClaim, idClaim);
        }

        private AuthenticationResult Authenticate(string login, long userId)
        {
            var token = _tokenGenerator.GetToken(login, userId);
            var refreshToken = _tokenGenerator.GetRefreshToken();

            var refreshTokenExpiration = DateTime.Now.AddMinutes(_authOptions.RefreshLifetime);
            
            // Пока - один юзер = один рефреш токен
            _userService.CleanRefreshTokens(userId);
            _userService.SaveRefreshToken(userId, refreshToken, refreshTokenExpiration);
            
            return new AuthenticationResult()
            {
                IsSuccess = true,
                RefreshToken = refreshToken,
                Token = token
            };
        }

        private AuthenticationResult Fail(string reason)
        {
            return new AuthenticationResult()
            {
                IsSuccess = false,
                Errors = new List<string>()
                {
                    reason
                }
            };
        }
    }
}