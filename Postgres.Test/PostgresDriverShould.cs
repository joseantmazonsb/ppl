﻿using PluggablePersistenceLayer.Core.Drivers;
using Sql.Test;
using Sql.Test.Models;

namespace Postgres.Test {
    public class PostgresDriverShould : SqlDriverShould {

        private IDriver _driver;

        protected override IDriver Driver =>
            _driver ??= new PostgresDriverBuilder(Constants.PostgresConnectionString)
                .WithDataset<User>()
                .WithDataset<Booking>()
                .Build();
    }
}