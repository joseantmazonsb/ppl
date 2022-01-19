using Microsoft.EntityFrameworkCore;
using Sql;

namespace Postgres {
    public class PostgresContext : SqlContext {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseNpgsql(ConnectionString, o => o.EnableRetryOnFailure())
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
    }
}