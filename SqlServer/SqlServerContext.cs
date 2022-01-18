using Microsoft.EntityFrameworkCore;
using Sql;

namespace SqlServer {
    public class SqlServerContext : SqlContext {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(ConnectionString)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
    }
}