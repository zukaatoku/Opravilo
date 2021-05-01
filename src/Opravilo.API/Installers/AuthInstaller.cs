using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Opravilo.API.Auth;
using Opravilo.API.Options;

namespace Opravilo.API.Installers
{
    public static class AuthInstaller
    {
        public static void InstallAuthentication(this IServiceCollection services, JwtAuthOptions authOptions, VkAuthOptions vkAuthOptions)
        {
            var tokenParametersCreator = new TokenValidationParametersCreator();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.RequireHttpsMetadata = false;
                    opt.TokenValidationParameters = tokenParametersCreator.Create(authOptions, true);
                })
                .AddVkontakte(vkOptions =>
                {
                    vkOptions.ClientId = vkAuthOptions.ClientId;
                    vkOptions.ClientSecret = vkAuthOptions.Secret;
                    
                    // vkOptions.CallbackPath = "/api/account/vkLogin";
                    vkOptions.Fields.Add("uid");
                    vkOptions.Fields.Add("first_name");
                    vkOptions.Fields.Add("last_name");
                });

            services.AddSingleton<ITokenValidationParametersCreator>(tokenParametersCreator);
            services.AddTransient<IAuthManager, AuthManager>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<ITokenGenerator, TokenGenerator>();
        }
    }
}