using System.Collections.Generic;
using PluggablePersistenceLayer.Core.Drivers;

namespace PluggablePersistenceLayer.Core.Builders {
    public abstract class DriverBuilder : IDriverBuilder {
        protected readonly HashSet<Dataset> Datasets;
        protected readonly string ConnectionString;

        protected DriverBuilder(string connectionString) {
            ConnectionString = connectionString;
            Datasets = new HashSet<Dataset>();
        }
        public abstract IDriver Build();
        public IDriverBuilder WithDataset<T>() where T : Entity {
            Datasets.Add(new Dataset(typeof(T)));
            return this;
        }

        public IDriverBuilder WithDataset<T>(string name) where T : Entity {
            Datasets.Add(new Dataset(typeof(T), name));
            return this;
        }
    }
}