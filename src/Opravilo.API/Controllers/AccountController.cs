using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Opravilo.API.Auth;
using Opravilo.API.Handlers;
using Opravilo.API.Models.Requests;
using Opravilo.API.Models.Responses;

namespace Opravilo.API.Controllers
{
    [Authorize]
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        private readonly IAuthManager _authManager;
        
        public AccountController(JwtTokenGenerator jwtTokenGenerator, IAuthManager authManager)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _authManager = authManager;
        }
        
        [HttpGet("login")]
        public string GetLogin()
        {
            return User?.Identity?.Name;
        }
        
        [AllowAnonymous]
        [HttpPost("login")]
        public string Login(
            [FromBody] LoginRequest request)
        {
            return _jwtTokenGenerator.AuthenticateUser(request.Login);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public AuthenticationResult RegisterUser(
            [FromBody] RegistrationRequest request)
        {
            return _authManager.Register(request.Login, request.Password);
        }
    }
}