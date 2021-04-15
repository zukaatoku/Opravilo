using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Opravilo.API.Handlers;
using Opravilo.API.Models.Requests;

namespace Opravilo.API.Controllers
{
    [Authorize]
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        
        public AccountController(JwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
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
            return _jwtTokenGenerator.AuthenticateUser(request.Login, request.Password);
        }
    }
}