using System;
using Core.Test;
using Core.Test.Models;
using FluentAssertions;
using PluggablePersistenceLayer.Core.Drivers;
using PluggablePersistenceLayer.MongoDb.Builders;
using Xunit;

namespace MongoDb.Test {
    public class MongoDriverShould : DriverShould {
        protected override IDriver Driver { get; } = new MongoDbDriverBuilder(Constants.ConnectionString)
            .WithDataset<User>()
            .WithDataset<Booking>()
            .Build();

        [Fact]
        public void WorkIfNoDatasetSpecifiedAndAllowAnyDatasetIsTrue() {
            var driver = new MongoDbDriverBuilder(Constants.ConnectionString)
                .WithOptions(new MongoDbDriverOptions {
                    Strict = false,
                    AllowAnyDataset = true
                })
                .Build();
            Action action = () => driver.GetAll<User>();
            action.Should().NotThrow<InvalidOperationException>();
        }
    }
}