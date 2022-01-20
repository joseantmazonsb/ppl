using Microsoft.EntityFrameworkCore.Infrastructure;
using PluggablePersistenceLayer.MySql;
using Sql.Test;
using Sql.Test.Models;
using Xunit;

namespace MySql.Test {
    public class MySqlDriverBuilderShould {

        [Fact]
        public void CreateMySqlDriver() {
            new MySqlDriverBuilder(Constants.MySqlConnectionString)
                .WithOptions<MySqlDbContextOptionsBuilder>(o => o.EnableRetryOnFailure())
                .WithDataset<User>()
                .WithDataset<Booking>()
                .Build();
        }
    }
}