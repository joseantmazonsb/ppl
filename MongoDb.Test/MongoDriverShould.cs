using Core.Drivers;
using Core.Test;
using MongoDb.Builders;

namespace MongoDb.Test {
    public class MongoDriverShould : DriverShould {
        protected override IDriver Driver { get; } = new MongoDbDriverBuilder(Constants.ConnectionString).Build();
    }
}