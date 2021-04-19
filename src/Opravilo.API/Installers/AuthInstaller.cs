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
            var validationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = authOptions.Issuer,

                ValidateAudience = true,
                ValidAudience = authOptions.Audience,

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                
                IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true
            };
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.RequireHttpsMetadata = false;
                    opt.TokenValidationParameters = validationParameters;
                });

            services.AddSingleton(validationParameters);
            services.AddTransient<IAuthManager, AuthManager>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<ITokenGenerator, TokenGenerator>();
        }
    }
}