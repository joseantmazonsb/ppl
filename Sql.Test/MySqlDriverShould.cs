using Core.Drivers;
using Sql.Builders;
using Sql.Test.Models;

namespace Sql.Test {
    public class MySqlDriverShould : SqlDriverShould {
        private IDriver _driver;
        protected override IDriver Driver =>
            _driver ??= new SqlDriverBuilder(Constants.MySqlConnectionString)
                .WithSet<User>()
                .WithSet<Booking>()
                .WithOptions(new SqlDriverBuilderOptions(SqlProvider.MySql))
                .Build();
    }
}