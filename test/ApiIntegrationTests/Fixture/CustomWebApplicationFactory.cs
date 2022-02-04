using System;
using System.Linq;
using ApiIntegrationTests.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Opravilo.API.Auth;
using Opravilo.API.BackgroundServices;
using Opravilo.DataAccess.EntityFramework;

namespace ApiIntegrationTests.Fixture;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            RemoveService(services, typeof(DbContextOptions<DataContext>));
            RemoveImplementation(services, typeof(MigratorService));

            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseInMemoryDatabase("InMemory");
            });

            var sp = services.BuildServiceProvider();
            using (var scope = sp.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DataContext>();
                var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
                TestDbInitializer.PopulateDb(db, passwordHasher);
            }
        });
    }

    private void RemoveService(IServiceCollection services, Type service)
    {
        var descriptor = services.Single(d => d.ServiceType == service);
        services.Remove(descriptor);
    }
    
    private void RemoveImplementation(IServiceCollection services, Type service)
    {
        var descriptor = services.Single(d => d.ImplementationType == service);
        services.Remove(descriptor);
    }
}