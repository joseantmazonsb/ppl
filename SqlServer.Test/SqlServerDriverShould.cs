using Core.Drivers;
using Sql.Test;
using Sql.Test.Models;

namespace SqlServer.Test {
    public class SqlServerDriverShould : SqlDriverShould {

        private IDriver _driver;

        protected override IDriver Driver =>
            _driver ??= new SqlServerDriverBuilder(Constants.MsSqlConnectionString)
                .WithSet<User>()
                .WithSet<Booking>()
                .Build();
    }
}