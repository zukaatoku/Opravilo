using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Opravilo.DataAccess.EntityFramework;

namespace Opravilo.API.Installers
{
    public static class DatabaseInstaller
    {
        public static void InstallDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Opravilo");
            services.AddDbContext<DataContext>(builder =>
            {
                builder.UseNpgsql(connectionString);
            });
        }
    }
}