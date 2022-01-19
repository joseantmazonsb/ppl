using PluggablePersistenceLayer.SqlServer;
using Sql.Test;
using Sql.Test.Models;
using Xunit;

namespace SqlServer.Test {
    public class SqlServerDriverBuilderShould {

        [Fact]
        public void CreateMsSqlDriver() {
            new SqlServerDriverBuilder(Constants.MsSqlConnectionString)
                .WithDataset<User>("users")
                .WithDataset<Booking>()
                .Build();
        }
    }
}