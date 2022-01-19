using Core.Test;
using Core.Test.Models;
using MongoDb.Builders;
using PluggablePersistenceLayer.Core.Drivers;

namespace MongoDb.Test {
    public class MongoDriverShould : DriverShould {
        protected override IDriver Driver { get; } = new MongoDbDriverBuilder(Constants.ConnectionString)
            .WithDataset<User>()
            .WithDataset<Booking>()
            .Build();
    }
}