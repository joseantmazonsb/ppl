using PluggablePersistenceLayer.Core.Drivers;
using Sql.Test;
using Sql.Test.Models;

namespace MySql.Test {
    public class MySqlDriverShould : SqlDriverShould {

        private IDriver _driver;

        protected override IDriver Driver =>
            _driver ??= new MySqlDriverBuilder(Constants.MySqlConnectionString)
                .WithDataset<User>()
                .WithDataset<Booking>()
                .Build();
    }
}