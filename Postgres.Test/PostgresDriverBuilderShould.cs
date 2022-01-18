﻿using Sql.Test;
using Sql.Test.Models;
using Xunit;

namespace Postgres.Test {
    public class PostgresDriverBuilderShould {
        [Fact]
        public void CreatePostgresDriver() {
            new PostgresDriverBuilder(Constants.PostgresConnectionString)
                .WithSet<User>()
                .WithSet<Booking>()
                .Build();
        }
    }
}