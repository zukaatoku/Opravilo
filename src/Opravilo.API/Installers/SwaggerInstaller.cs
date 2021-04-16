using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Opravilo.API.Installers
{
    public static class SwaggerInstaller
    {
        public static void InstallSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Opravilo.API", Version = "v1" });
                
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Bearer",
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Description = "Write 'token' to authenticate",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http
                });
                
                var securityScheme = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                };

                var securityRequirements = new OpenApiSecurityRequirement()
                {
                    { securityScheme, new string[] { } },
                };
                c.AddSecurityRequirement(securityRequirements);
            });
        }
    }
}