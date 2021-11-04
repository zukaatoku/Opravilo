using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Opravilo.API.Extensions
{
    public static class ControllerExtensions
    {
        public static long GetUserId(this ControllerBase controller)
        {
            var user = controller.User.Identity as ClaimsIdentity;
            return Convert.ToInt64(user?.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}