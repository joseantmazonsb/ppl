using Microsoft.EntityFrameworkCore;
using Sql;
using Sql.Builders;

namespace MySql {
    public class MySqlDriverBuilder : SqlDriverBuilder {
        public MySqlDriverBuilder(string connectionString) : base(connectionString) {}

        protected override SqlContext CreateContext() {
            return Utils.CreateDbContext<MySqlContext>(Sets);
        }
    }
}