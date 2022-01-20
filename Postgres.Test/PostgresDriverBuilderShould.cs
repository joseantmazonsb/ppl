using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using PluggablePersistenceLayer.Postgres;
using Sql.Test;
using Sql.Test.Models;
using Xunit;

namespace Postgres.Test {
    public class PostgresDriverBuilderShould {
        [Fact]
        public void CreatePostgresDriver() {
            new PostgresDriverBuilder(Constants.PostgresConnectionString)
                .WithOptions<NpgsqlDbContextOptionsBuilder>(o => o.EnableRetryOnFailure())
                .WithDataset<User>()
                .WithDataset<Booking>()
                .Build();
        }
    }
}