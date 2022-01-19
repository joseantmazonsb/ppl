using System;
using System.Collections.Generic;
using System.Linq;
using PluggablePersistenceLayer.Core.Drivers;

namespace PluggablePersistenceLayer.Core.Builders {
    public abstract class DriverBuilder : IDriverBuilder {
        protected readonly HashSet<Dataset> Datasets;
        protected readonly string ConnectionString;

        protected DriverBuilder(string connectionString) {
            ConnectionString = connectionString;
            Datasets = new HashSet<Dataset>();
        }
        private void BeforeBuild() {
            var errorTitle = $"Cannot build {nameof(IDriverBuilder)} instance:";
            if (string.IsNullOrEmpty(ConnectionString)) {
                throw new InvalidOperationException($"{errorTitle} the connection string cannot be null nor empty!");
            }
            if (!Datasets.Any()) {
                throw new InvalidOperationException($"{errorTitle} there are no datasets! " +
                                                    $"Use method '{nameof(WithDataset)}' to specify the datasets.");
            }
        }

        public IDriver Build() {
            BeforeBuild();
            return DoBuild();
        }
        protected abstract IDriver DoBuild();
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