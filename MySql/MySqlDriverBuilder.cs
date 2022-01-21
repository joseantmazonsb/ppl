using PluggablePersistenceLayer.Core.Drivers;
using PluggablePersistenceLayer.Sql.Builders;

namespace PluggablePersistenceLayer.MySql {
    /// <summary>
    /// Wrapper to interact with MySql and MariaDb databases.
    /// </summary>
    public class MySqlDriverBuilder : SqlDriverBuilder {
        public MySqlDriverBuilder(string connectionString) : base(connectionString) {}

        protected override IDriver Build() {
            return new MySqlDriver(ConnectionString, Datasets, Options, OnModelCreating);
        }
    }
}