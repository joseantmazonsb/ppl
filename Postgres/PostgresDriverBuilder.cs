using PluggablePersistenceLayer.Sql;
using PluggablePersistenceLayer.Sql.Builders;

namespace PluggablePersistenceLayer.Postgres {
    /// <summary>
    /// Wrapper to interact with PostgreSql databases.
    /// </summary>
    public class PostgresDriverBuilder : SqlDriverBuilder {
        public PostgresDriverBuilder(string connectionString) : base(connectionString) {}

        protected override SqlContext CreateContext() {
            return Utils.CreateDbContext<PostgresContext>(Datasets);
        }
    }
}