using Microsoft.EntityFrameworkCore;
using Sql;

namespace PluggablePersistenceLayer.SqlServer {
    public class SqlServerContext : SqlContext {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(ConnectionString, o => o.EnableRetryOnFailure())
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
    }
}