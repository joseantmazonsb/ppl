using Microsoft.EntityFrameworkCore;
using PluggablePersistenceLayer.Sql;

namespace PluggablePersistenceLayer.MySql {
    public class MySqlContext : SqlContext {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString), Options)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
    }
}