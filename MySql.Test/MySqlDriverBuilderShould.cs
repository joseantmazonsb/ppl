using Sql.Test;
using Sql.Test.Models;
using Xunit;

namespace MySql.Test {
    public class MySqlDriverBuilderShould {

        [Fact]
        public void CreateMySqlDriver() {
            new MySqlDriverBuilder(Constants.MySqlConnectionString)
                .WithSet<User>()
                .WithSet<Booking>()
                .Build();
        }
    }
}