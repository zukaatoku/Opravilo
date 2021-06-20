using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Opravilo.API.Auth;
using Opravilo.API.Auth.External;
using Opravilo.API.Options;

namespace Opravilo.API.Installers
{
    public static class AuthInstaller
    {
        public static void InstallAuthentication(this IServiceCollection services, JwtAuthOptions authOptions)
        {
            var tokenParametersCreator = new TokenValidationParametersCreator();

            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.RequireHttpsMetadata = false;
                    opt.TokenValidationParameters = tokenParametersCreator.Create(authOptions, true);
                });

            services.AddSingleton<ITokenValidationParametersCreator>(tokenParametersCreator);
            services.AddTransient<IExternalAuth, VkontakteExternalAuth>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<ITokenGenerator, TokenGenerator>();
        }
    }
}