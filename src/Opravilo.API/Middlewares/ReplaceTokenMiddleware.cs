using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Opravilo.API.Middlewares
{
    public class ReplaceTokenMiddleware
    {
        private readonly RequestDelegate _next;

        public ReplaceTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Cookies.ContainsKey("X-AUTH-TOKEN"))
            {
                var jwt = context.Request.Cookies["X-AUTH-TOKEN"];

                context.Request.Headers.Append("Authorization", $"Bearer {jwt}");
            }

            await _next(context);
        }
    }
}