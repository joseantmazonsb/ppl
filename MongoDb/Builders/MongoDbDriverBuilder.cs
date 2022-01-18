using Core.Builders;
using Core.Drivers;

namespace MongoDb.Builders {
    public class MongoDbDriverBuilder : IDriverBuilder {
        
        private readonly string _connectionString;

        public MongoDbDriverBuilder(string connectionString) {
            _connectionString = connectionString;
        }
        
        public IDriver Build() {
            return new MongoDbDriver(_connectionString);
        }
    }
}