using Microsoft.EntityFrameworkCore;
using Sql;

namespace MySql {
    public class MySqlContext : SqlContext {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
    }
}