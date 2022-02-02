using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Opravilo.Migrator;

namespace Opravilo.API.BackgroundServices
{
    public class MigratorService : IHostedService
    {
        private readonly string _connectionString;
        
        public MigratorService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Opravilo");
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var migrator = new DbMigrator(_connectionString);
            migrator.MigrateDb();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}