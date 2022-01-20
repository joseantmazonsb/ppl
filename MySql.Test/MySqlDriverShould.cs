﻿using Microsoft.EntityFrameworkCore.Infrastructure;
using PluggablePersistenceLayer.Core.Drivers;
using PluggablePersistenceLayer.MySql;
using Sql.Test;
using Sql.Test.Models;

namespace MySql.Test {
    public class MySqlDriverShould : SqlDriverShould {

        private IDriver _driver;

        protected override IDriver Driver =>
            _driver ??= new MySqlDriverBuilder(Constants.MySqlConnectionString)
                .WithOptions<MySqlDbContextOptionsBuilder>(o => o.EnableRetryOnFailure())
                .WithDataset<User>()
                .WithDataset<Booking>()
                .Build();
    }
}