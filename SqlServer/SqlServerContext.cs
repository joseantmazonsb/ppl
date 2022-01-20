using Microsoft.EntityFrameworkCore;
using PluggablePersistenceLayer.Sql;

namespace PluggablePersistenceLayer.SqlServer {
    public class SqlServerContext : SqlContext {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(ConnectionString, Options)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
    }
}