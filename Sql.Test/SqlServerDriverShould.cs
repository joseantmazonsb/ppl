using Core.Drivers;
using Sql.Builders;
using Sql.Test.Models;

namespace Sql.Test {
    public class SqlServerDriverShould : SqlDriverShould {

        private IDriver _driver;

        protected override IDriver Driver =>
            _driver ??= new SqlDriverBuilder(Constants.MsSqlConnectionString)
                .WithSet<User>()
                .WithSet<Booking>()
                .WithOptions(new SqlDriverBuilderOptions(SqlProvider.MsSqlServer))
                .Build();
    }
}