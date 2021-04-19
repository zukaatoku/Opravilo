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
        
        public AccountController(IAuthManager authManager)
        {
            _authManager = authManager;
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
            return _authManager.Authenticate(request.Login, request.Password);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public AuthenticationResult RegisterUser(
            [FromBody] RegistrationRequest request)
        {
            return _authManager.Register(request.Login, request.Password);
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public AuthenticationResult RefreshToken(string token, string refreshToken)
        {
            return _authManager.RefreshToken(token, refreshToken);
        }
    }
}