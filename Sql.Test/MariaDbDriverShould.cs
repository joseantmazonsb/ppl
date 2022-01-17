using Core.Drivers;
using Sql.Builders;
using Sql.Test.Models;

namespace Sql.Test {
    public class MariaDbDriverShould : SqlDriverShould {
        private IDriver _driver;
        protected override IDriver Driver =>
            _driver ??= new SqlDriverBuilder(Constants.MariaDbConnectionString)
                .WithSet<User>()
                .WithSet<Booking>()
                .WithOptions(new SqlDriverBuilderOptions(SqlProvider.MariaDb))
                .Build();
    }
}