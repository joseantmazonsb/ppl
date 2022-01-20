using System;
using Core.Test.Models;
using FluentAssertions;
using PluggablePersistenceLayer.MongoDb.Builders;
using Xunit;

namespace MongoDb.Test {
    public class MongoDbDriverBuilderShould {
        [Fact]
        public void CreateMongoDbDriver() {
            Action action = () => new MongoDbDriverBuilder(Constants.ConnectionString)
                .WithDataset<User>()
                .WithDataset<Booking>()
                .Build();
            action.Should().NotThrow();
        }
    }
}