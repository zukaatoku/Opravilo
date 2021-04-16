using Microsoft.Extensions.DependencyInjection;
using Opravilo.Application.Interfaces.Services;

namespace Opravilo.API.Installers
{
    public static class ApplicationInstaller
    {
        public static void InstallApplication(this IServiceCollection services)
        {
            // services.AddTransient<IUserService>()
        }
    }
}