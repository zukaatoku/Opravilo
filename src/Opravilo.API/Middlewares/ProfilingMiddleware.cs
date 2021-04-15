using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Opravilo.API.Middlewares
{
    public class ProfilingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ProfilingMiddleware> _logger;

        public ProfilingMiddleware(RequestDelegate next, ILogger<ProfilingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = new Stopwatch();

            sw.Start();
            await _next(context);
            sw.Stop();

            _logger.LogInformation(
                "Request: {@Path} Elapsed time: {@Elapsed} Total memory: {@Memory} mb",
                context.Request.Path.ToString(),
                sw.Elapsed.ToString(),
                GC.GetTotalMemory(false) / 1024 / 1024);
        }
    }
}