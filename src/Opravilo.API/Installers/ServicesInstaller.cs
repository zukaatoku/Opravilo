using Microsoft.Extensions.DependencyInjection;
using Opravilo.API.BackgroundServices;

namespace Opravilo.API.Installers
{
    public static class ServicesInstaller
    {
        public static void InstallServices(this IServiceCollection services)
        {
             services.AddHostedService<MigratorService>();
        }
    }
}