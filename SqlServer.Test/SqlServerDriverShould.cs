using PluggablePersistenceLayer.Core.Drivers;
using PluggablePersistenceLayer.SqlServer;
using Sql.Test;
using Sql.Test.Models;

namespace SqlServer.Test {
    public class SqlServerDriverShould : SqlDriverShould {

        private IDriver _driver;

        protected override IDriver Driver =>
            _driver ??= new SqlServerDriverBuilder(Constants.MsSqlConnectionString)
                .WithDataset<User>()
                .WithDataset<Booking>()
                .Build();
    }
}