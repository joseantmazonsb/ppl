using PluggablePersistenceLayer.Core.Drivers;
using PluggablePersistenceLayer.Sql.Builders;

namespace PluggablePersistenceLayer.Sqlite {
    /// <summary>
    /// Wrapper to interact with Microsoft SqlServer databases.
    /// </summary>
    public class SqliteDriverBuilder : SqlDriverBuilder {
        public SqliteDriverBuilder(string connectionString) : base(connectionString) {
        }

        protected override IDriver Build() {
            return new SqliteDriver(ConnectionString, Datasets, Options, OnModelCreating);
        }
    }
}