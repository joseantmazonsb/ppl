using MySql;
using Sql.Test;
using Sql.Test.Models;
using Xunit;

namespace MariaDb.Test {
    public class MariaDbDriverBuilderShould {
        [Fact]
        public void CreateMariaDbDriver() {
            new MySqlDriverBuilder(Constants.MariaDbConnectionString)
                .WithSet<User>()
                .WithSet<Booking>()
                .Build();
        }
    }
}