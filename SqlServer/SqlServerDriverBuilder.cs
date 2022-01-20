using PluggablePersistenceLayer.Sql;
using PluggablePersistenceLayer.Sql.Builders;

namespace PluggablePersistenceLayer.SqlServer {
    /// <summary>
    /// Wrapper to interact with Microsoft SqlServer databases.
    /// </summary>
    public class SqlServerDriverBuilder : SqlDriverBuilder {
        public SqlServerDriverBuilder(string connectionString) : base(connectionString) {
        }

        protected override SqlContext CreateContext() {
            var ctx = Utils.CreateDbContext<SqlServerContext>(Datasets);
            ctx.Options = Options;
            return ctx;
        }
    }
}