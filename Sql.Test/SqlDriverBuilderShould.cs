using Sql.Builders;
using Sql.Test.Models;
using Xunit;

namespace Sql.Test {
    public class SqlDriverBuilderShould {

        [Fact]
        public void CreateMsSqlDriver() {
            new SqlDriverBuilder(Constants.MsSqlConnectionString)
                .WithSet<User>()
                .WithSet<Booking>()
                .WithOptions(new SqlDriverBuilderOptions(SqlProvider.MsSqlServer))
                .Build();
        }
        
        [Fact]
        public void CreateMySqlDriver() {
            new SqlDriverBuilder(Constants.MySqlConnectionString)
                .WithSet<User>()
                .WithSet<Booking>()
                .WithOptions(new SqlDriverBuilderOptions(SqlProvider.MySql))
                .Build();
        }
        
        [Fact]
        public void CreateMariaDbDriver() {
            new SqlDriverBuilder(Constants.MariaDbConnectionString)
                .WithSet<User>()
                .WithSet<Booking>()
                .WithOptions(new SqlDriverBuilderOptions(SqlProvider.MariaDb))
                .Build();
        }
        
        [Fact]
        public void CreatePostgresDriver() {
            new SqlDriverBuilder(Constants.PostgresConnectionString)
                .WithSet<User>()
                .WithSet<Booking>()
                .WithOptions(new SqlDriverBuilderOptions(SqlProvider.Postgres))
                .Build();
        }
        
    }
}