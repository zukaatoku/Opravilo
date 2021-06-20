using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Opravilo.API.Auth;
using Opravilo.API.Auth.External;
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
        private readonly IExternalAuth _externalAuth;
        
        public AccountController(IUserManager authManager, IPasswordHasher passwordHasher, IExternalAuth externalAuth)
        {
            _authManager = authManager;
            _passwordHasher = passwordHasher;
            _externalAuth = externalAuth;
        }

        [AllowAnonymous]
        [HttpGet("loginVK")]
        public async Task<AuthenticationResult> LoginVk(string code)
        {
            var externalInfo = await _externalAuth.Validate(code);
            var userExists = _authManager.UserExists(externalInfo.user_id);

            if (userExists)
            {
                return _authManager.AuthenticateVkontakte(externalInfo.user_id);
            }
            
            var credentials = await _externalAuth.GetUserInfo(externalInfo.user_id, externalInfo.access_token);
            return _authManager.CreateAndAuthenticate(credentials.id.ToString(), credentials.first_name, credentials.last_name);
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