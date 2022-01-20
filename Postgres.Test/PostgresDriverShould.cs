using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using PluggablePersistenceLayer.Core.Drivers;
using PluggablePersistenceLayer.Postgres;
using Sql.Test;
using Sql.Test.Models;

namespace Postgres.Test {
    public class PostgresDriverShould : SqlDriverShould {

        private IDriver _driver;

        protected override IDriver Driver =>
            _driver ??= new PostgresDriverBuilder(Constants.PostgresConnectionString)
                .WithOptions<NpgsqlDbContextOptionsBuilder>(o => o.EnableRetryOnFailure())
                .WithDataset<User>()
                .WithDataset<Booking>()
                .Build();
    }
}