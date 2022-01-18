using System;
using FluentAssertions;
using MongoDb.Builders;
using Xunit;

namespace MongoDb.Test {
    public class MongoDbDriverBuilderShould {
        [Fact]
        public void CreateMongoDbDriver() {
            Action action = () => new MongoDbDriverBuilder(Constants.ConnectionString).Build();
            action.Should().NotThrow();
        }
    }
}