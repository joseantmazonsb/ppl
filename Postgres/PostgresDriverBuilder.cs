using Sql;
using Sql.Builders;

namespace Postgres {
    public class PostgresDriverBuilder : SqlDriverBuilder {
        public PostgresDriverBuilder(string connectionString) : base(connectionString) {}

        protected override SqlContext CreateContext() {
            return Utils.CreateDbContext<PostgresContext>(Sets);
        }
    }
}