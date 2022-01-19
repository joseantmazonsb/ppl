using MySql;
using PluggablePersistenceLayer.Core.Drivers;
using Sql.Test;
using Sql.Test.Models;

namespace MariaDb.Test {
    public class MariaDbDriverShould : SqlDriverShould {

        private IDriver _driver;

        protected override IDriver Driver =>
            _driver ??= new MySqlDriverBuilder(Constants.MariaDbConnectionString)
                .WithDataset<User>()
                .WithDataset<Booking>()
                .Build();
    }
}