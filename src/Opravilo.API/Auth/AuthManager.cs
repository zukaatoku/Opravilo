using System.Collections.Generic;
using Opravilo.API.Models.Responses;
using Opravilo.Application.Interfaces.Services;

namespace Opravilo.API.Auth
{
    public class AuthManager : IAuthManager
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasher _hasher;
        private readonly ITokenGenerator _tokenGenerator;
        
        public AuthManager(IUserService userService, IPasswordHasher hasher, ITokenGenerator tokenGenerator)
        {
            _userService = userService;
            _hasher = hasher;
            _tokenGenerator = tokenGenerator;
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

            var token = _tokenGenerator.GetToken(user.Login);

            return new AuthenticationResult()
            {
                IsSuccess = true,
                Token = token
            };
        }
    }
}