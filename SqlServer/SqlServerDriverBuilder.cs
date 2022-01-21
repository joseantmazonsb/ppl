using PluggablePersistenceLayer.Core.Drivers;
using PluggablePersistenceLayer.Sql.Builders;

namespace PluggablePersistenceLayer.SqlServer {
    /// <summary>
    /// Wrapper to interact with Microsoft SqlServer databases.
    /// </summary>
    public class SqlServerDriverBuilder : SqlDriverBuilder {
        public SqlServerDriverBuilder(string connectionString) : base(connectionString) {
        }

        protected override IDriver Build() {
            return new SqlServerDriver(ConnectionString, Datasets, Options, OnModelCreating);
        }
    }
}