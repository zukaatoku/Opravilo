using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Opravilo.API.BackgroundServices;
using Opravilo.DataAccess.EntityFramework;

namespace ApiIntegrationTests;

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