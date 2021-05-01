using System.Linq;
using System.Security.Claims;
using AspNet.Security.OAuth.Vkontakte;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
        private readonly IUserManager _authManager;
        private readonly IPasswordHasher _passwordHasher;
        
        public AccountController(IUserManager authManager, IPasswordHasher passwordHasher)
        {
            _authManager = authManager;
            _passwordHasher = passwordHasher;
        }
        
        [HttpGet("vkLogin")]
        [Authorize(AuthenticationSchemes = VkontakteAuthenticationDefaults.AuthenticationScheme)]
        public AuthenticationResult AuthenticateVkontakte()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            
            var id = claimsIdentity.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var firstName = claimsIdentity.Claims.First(c => c.Type == ClaimTypes.GivenName).Value;
            var secondName = claimsIdentity.Claims.First(c => c.Type == ClaimTypes.Surname).Value;
            
            return _authManager.AuthenticateOrCreate(id, firstName, secondName);
        }
        
        [HttpGet("login")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
            return _authManager.Register(request.Login, request.DisplayName, hashedPassword);
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public AuthenticationResult RefreshToken(string token, string refreshToken)
        {
            return _authManager.RefreshToken(token, refreshToken);
        }
    }
}