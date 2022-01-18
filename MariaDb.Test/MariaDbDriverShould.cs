using Core.Drivers;
using MySql;
using Sql.Test;
using Sql.Test.Models;

namespace MariaDb.Test {
    public class MariaDbDriverShould : SqlDriverShould {

        private IDriver _driver;

        protected override IDriver Driver =>
            _driver ??= new MySqlDriverBuilder(Constants.MariaDbConnectionString)
                .WithSet<User>()
                .WithSet<Booking>()
                .Build();
    }
}