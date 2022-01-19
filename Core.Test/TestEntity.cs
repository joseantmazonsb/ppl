using System;
using PluggablePersistenceLayer.Core;

namespace Core.Test {
    public class TestEntity : Entity {
        public override Guid Id { get; set; }
    }
}