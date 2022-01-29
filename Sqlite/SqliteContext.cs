using Microsoft.EntityFrameworkCore;
using PluggablePersistenceLayer.Sql;

namespace PluggablePersistenceLayer.Sqlite {
    public class SqliteContext : SqlContext {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite(ConnectionString, Options)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
    }
}