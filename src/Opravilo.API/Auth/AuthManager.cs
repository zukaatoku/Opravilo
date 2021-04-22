using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Opravilo.API.Models.Responses;
using Opravilo.API.Options;
using Opravilo.Application.Interfaces.Services;

namespace Opravilo.API.Auth
{
    public class AuthManager : IAuthManager
    {
        private readonly IUserService _userService;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly ITokenValidationParametersCreator _tokenParametersCreator;
        private readonly AuthOptions _authOptions;
        
        public AuthManager(IUserService userService, ITokenGenerator tokenGenerator, 
            ITokenValidationParametersCreator tokenParametersCreator, AuthOptions authOptions)
        {
            _userService = userService;
            _tokenGenerator = tokenGenerator;
            _authOptions = authOptions;
            _tokenParametersCreator = tokenParametersCreator;
        }
        
        public AuthenticationResult Register(string login, string email, string hashedPassword)
        {
            var registrationResult = _userService.RegisterUser(login, email, hashedPassword);

            if (!registrationResult.IsSuccess)
            {
                return Fail(registrationResult.Errors.First());
            }

            var user = registrationResult.CreatedUser;
            
            return Authenticate(user.Login, user.Id);
        }

        public AuthenticationResult Authenticate(string login, string hashedPassword)
        {
            var user = _userService.FindUser(login, hashedPassword);

            if (user == null)
            {
                return Fail("Failed to find user!");
            }

            return Authenticate(user.Login, user.Id);
        }

        public AuthenticationResult RefreshToken(string jwtToken, string refreshToken)
        {
            var parameters = _tokenParametersCreator.Create(_authOptions, false);

            var principal =
                new JwtSecurityTokenHandler().ValidateToken(jwtToken, parameters, out var validatedToken);
            
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