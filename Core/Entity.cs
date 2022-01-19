using System;

namespace PluggablePersistenceLayer.Core {
    public abstract class Entity {
        public abstract Guid Id { get; set; }
    }
    
}