using System;
using System.IO;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Opravilo.Migrator.Migrations;
using Serilog;

namespace Opravilo.Migrator
{
    public class DbMigrator
    {
        private readonly string _connectionString;

        public DbMigrator(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void MigrateDb()
        {
            var serviceProvider = CreateServices();

            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }
        
        private IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres()
                    .WithGlobalConnectionString(_connectionString)
                    .ScanIn(typeof(AddUsersTable).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddSerilog())
                .BuildServiceProvider(false);
        }

        private void UpdateDatabase(IServiceProvider serviceProvider)
        {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            // Execute the migrations
            runner.MigrateUp();

#if DEBUG
            var sql = File.ReadAllText("Scripts/Seed.sql");
            runner.Processor.Execute(sql);
#endif
        }
    }
}