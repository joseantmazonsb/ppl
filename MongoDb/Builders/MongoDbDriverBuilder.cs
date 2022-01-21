using PluggablePersistenceLayer.Core.Builders;
using PluggablePersistenceLayer.Core.Drivers;

namespace PluggablePersistenceLayer.MongoDb.Builders {
    public class MongoDbDriverBuilder : DriverBuilder {
        private MongoDbDriverOptions _options;
        public MongoDbDriverBuilder(string connectionString) : base(connectionString) {
        }

        public override IDriver Build(bool withDatabaseCreated = true) {
            var driver = new MongoDbDriver(ConnectionString, Datasets, _options);
            if (withDatabaseCreated) {
                driver.EnsureDatabaseCreated();
            }
            return driver;
        }

        /// <summary>
        /// Allows specific MongoDb configuration to be set. 
        /// </summary>
        /// <param name="options">MongoDb settings.</param>
        /// <returns>This builder</returns>
        public IDriverBuilder WithOptions(MongoDbDriverOptions options) {
            _options = options;
            return this;
        }
    }
}