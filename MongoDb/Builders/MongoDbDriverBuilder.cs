using PluggablePersistenceLayer.Core.Builders;
using PluggablePersistenceLayer.Core.Drivers;

namespace MongoDb.Builders {
    public class MongoDbDriverBuilder : DriverBuilder {
        
        public MongoDbDriverBuilder(string connectionString) : base(connectionString) { }

        protected override IDriver DoBuild() {
            return new MongoDbDriver(ConnectionString, Datasets);
        }
    }
}