using System;
using System.Runtime.CompilerServices;
using DynamicProxy.Factories;
using Microsoft.EntityFrameworkCore;

[assembly: InternalsVisibleTo(DynamicTypeFactory.DynamicAssemblyName)]
namespace Sql {
    internal class SqlContext : DbContext {
        public string ConnectionString { get; set; } = string.Empty;
        public SqlProvider Provider { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            switch (Provider) {
                case SqlProvider.MsSqlServer:
                    optionsBuilder.UseSqlServer(ConnectionString);
                    break;
                case SqlProvider.MySql:
                case SqlProvider.MariaDb:
                    optionsBuilder.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString));
                    break;
                case SqlProvider.Postgres:
                    optionsBuilder.UseNpgsql(ConnectionString);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(Provider), "Provider not supported.");
            }
            optionsBuilder.EnableSensitiveDataLogging().EnableDetailedErrors();
        }
    }
}