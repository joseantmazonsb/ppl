using Microsoft.EntityFrameworkCore.Infrastructure;
using PluggablePersistenceLayer.Core.Drivers;
using PluggablePersistenceLayer.MySql;
using Sql.Test;
using Sql.Test.Models;

namespace MariaDb.Test {
    public class MariaDbDriverShould : SqlDriverShould {

        private IDriver _driver;

        protected override IDriver Driver =>
            _driver ??= new MySqlDriverBuilder(Constants.MariaDbConnectionString)
                .WithOptions<MySqlDbContextOptionsBuilder>(o => o.EnableRetryOnFailure())
                .WithDataset<User>()
                .WithDataset<Booking>()
                .Build();
    }
}