using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Opravilo.API.Controllers
{
    [Authorize]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("displayName")]
        public string GetDisplayName()
        {
            var user = User.Identity as ClaimsIdentity;
            return user.Claims.First(c => c.Type == "nickname").Value;
        }
    }
}