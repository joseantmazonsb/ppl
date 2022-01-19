using MySql;
using Sql.Test;
using Sql.Test.Models;
using Xunit;

namespace MariaDb.Test {
    public class MariaDbDriverBuilderShould {
        [Fact]
        public void CreateMariaDbDriver() {
            new MySqlDriverBuilder(Constants.MariaDbConnectionString)
                .WithDataset<User>()
                .WithDataset<Booking>()
                .Build();
        }
    }
}