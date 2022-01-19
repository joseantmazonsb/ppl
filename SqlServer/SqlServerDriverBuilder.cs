using Sql;
using Sql.Builders;

namespace PluggablePersistenceLayer.SqlServer {
    public class SqlServerDriverBuilder : SqlDriverBuilder {
        public SqlServerDriverBuilder(string connectionString) : base(connectionString) {}

        protected override SqlContext CreateContext() {
            return Utils.CreateDbContext<SqlServerContext>(Datasets);
        }
    }
}