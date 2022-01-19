using System;

namespace PluggablePersistenceLayer.Core {
    public class Dataset {
        public Type Type { get; }
        public string Name { get; }

        public Dataset(Type type) {
            Type = type;
            Name = type.Name;
        }
        
        public Dataset(Type type, string name) {
            Type = type;
            Name = name;
        }
    }
}