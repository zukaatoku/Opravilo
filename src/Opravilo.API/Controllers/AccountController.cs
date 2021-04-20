using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Opravilo.API.Auth;
using Opravilo.API.Models.Requests;
using Opravilo.API.Models.Responses;

namespace Opravilo.API.Controllers
{
    [Authorize]
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        private readonly IPasswordHasher _passwordHasher;
        
        public AccountController(IAuthManager authManager, IPasswordHasher passwordHasher)
        {
            _authManager = authManager;
            _passwordHasher = passwordHasher;
        }
        
        [HttpGet("login")]
        public string GetLogin()
        {
            return User?.Identity?.Name;
        }
        
        [AllowAnonymous]
        [HttpPost("login")]
        public AuthenticationResult Login(
            [FromBody] LoginRequest request)
        {
            var hashedPassword = _passwordHasher.HashPassword(request.Password);
            return _authManager.Authenticate(request.Login, hashedPassword);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public AuthenticationResult RegisterUser(
            [FromBody] RegistrationRequest request)
        {
            var hashedPassword = _passwordHasher.HashPassword(request.Password);
            return _authManager.Register(request.Login, hashedPassword);
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public AuthenticationResult RefreshToken(string token, string refreshToken)
        {
            return _authManager.RefreshToken(token, refreshToken);
        }
    }
}