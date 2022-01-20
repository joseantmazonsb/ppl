using PluggablePersistenceLayer.Core.Builders;
using PluggablePersistenceLayer.Core.Drivers;

namespace PluggablePersistenceLayer.MongoDb.Builders {
    public class MongoDbDriverBuilder : DriverBuilder {
        private MongoDbDriverOptions _options;
        public MongoDbDriverBuilder(string connectionString) : base(connectionString) {
        }

        public override IDriver Build() {
            return new MongoDbDriver(ConnectionString, Datasets, _options);
        }

        public IDriverBuilder WithOptions(MongoDbDriverOptions options) {
            _options = options;
            return this;
        }
    }
}