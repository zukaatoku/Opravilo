using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Opravilo.API.Auth;
using Opravilo.API.Options;

namespace Opravilo.API.Installers
{
    public static class AuthInstaller
    {
        public static void InstallJwt(this IServiceCollection services, AuthOptions authOptions)
        {
            var tokenParametersCreator = new TokenValidationParametersCreator();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.RequireHttpsMetadata = false;
                    opt.TokenValidationParameters = tokenParametersCreator.Create(authOptions, true);
                });

            services.AddSingleton<ITokenValidationParametersCreator>(tokenParametersCreator);
            services.AddTransient<IAuthManager, AuthManager>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<ITokenGenerator, TokenGenerator>();
        }
    }
}