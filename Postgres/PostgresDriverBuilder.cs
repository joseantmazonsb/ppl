using PluggablePersistenceLayer.Core.Drivers;
using PluggablePersistenceLayer.Sql.Builders;

namespace PluggablePersistenceLayer.Postgres {
    /// <summary>
    /// Wrapper to interact with PostgreSql databases.
    /// </summary>
    public class PostgresDriverBuilder : SqlDriverBuilder {
        public PostgresDriverBuilder(string connectionString) : base(connectionString) {}
        public override IDriver Build() {
            return new PostgresDriver(ConnectionString, Datasets, Options);
        }
    }
}