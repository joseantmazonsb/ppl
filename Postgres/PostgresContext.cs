using Microsoft.EntityFrameworkCore;
using PluggablePersistenceLayer.Sql;

namespace PluggablePersistenceLayer.Postgres {
    public class PostgresContext : SqlContext {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseNpgsql(ConnectionString, Options)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
    }
}