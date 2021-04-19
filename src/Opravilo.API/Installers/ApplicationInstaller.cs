using Microsoft.Extensions.DependencyInjection;
using Opravilo.Application.Interfaces.Services;
using Opravilo.Application.Services;
using Opravilo.DataAccess.EntityFramework.Repositories;
using Opravilo.DataAccess.Repositories;

namespace Opravilo.API.Installers
{
    public static class ApplicationInstaller
    {
        public static void InstallApplication(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}