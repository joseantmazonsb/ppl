using PluggablePersistenceLayer.Sql;
using PluggablePersistenceLayer.Sql.Builders;

namespace PluggablePersistenceLayer.MySql {
    /// <summary>
    /// Wrapper to interact with MySql and MariaDb databases.
    /// </summary>
    public class MySqlDriverBuilder : SqlDriverBuilder {
        public MySqlDriverBuilder(string connectionString) : base(connectionString) {}

        protected override SqlContext CreateContext() {
            return Utils.CreateDbContext<MySqlContext>(Datasets);
        }
    }
}