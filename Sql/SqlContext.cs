using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace PluggablePersistenceLayer.Sql {
    /// <summary>
    /// Base context used to interact with the inner data layer.
    /// </summary>
    public abstract class SqlContext : DbContext {
        public string ConnectionString { get; set; } = string.Empty;
        public Action<IRelationalDbContextOptionsBuilderInfrastructure> Options;
        public Action<ModelBuilder> ModelCreatingAction;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            ModelCreatingAction?.Invoke(modelBuilder);
        }
    }
}