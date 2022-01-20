using Microsoft.EntityFrameworkCore.Infrastructure;
using PluggablePersistenceLayer.MySql;
using Sql.Test;
using Sql.Test.Models;
using Xunit;

namespace MariaDb.Test {
    public class MariaDbDriverBuilderShould {
        [Fact]
        public void CreateMariaDbDriver() {
            new MySqlDriverBuilder(Constants.MariaDbConnectionString)
                .WithOptions<MySqlDbContextOptionsBuilder>(o => o.EnableRetryOnFailure())
                .WithDataset<User>()
                .WithDataset<Booking>()
                .Build();
        }
    }
}