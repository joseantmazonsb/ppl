using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PluggablePersistenceLayer.Core.Builders;
using PluggablePersistenceLayer.Core.Drivers;

namespace PluggablePersistenceLayer.Sql.Builders {
    public abstract class SqlDriverBuilder : DriverBuilder {
        protected Action<IRelationalDbContextOptionsBuilderInfrastructure> Options;
        protected Action<ModelBuilder> OnModelCreating;

        protected SqlDriverBuilder(string connectionString) : base(connectionString) {
        }

        /// <summary>
        /// Allows relational database specific configuration to be performed on <c>DbContextOptions</c>.
        /// </summary>
        /// <param name="options">Provider specific options.</param>
        /// <typeparam name="T">A subclass of <c>IRelationalDbContextOptionsBuilderInfrastructure</c></typeparam>
        /// <returns>This builder</returns>
        public SqlDriverBuilder WithOptions<T>(Action<T> options)
            where T : IRelationalDbContextOptionsBuilderInfrastructure {
            if (Options != default) {
                throw new InvalidOperationException("Only one options action is allowed.");
            }
            Options = o => options((T) o);
            return this;
        }
        
        /// <summary>
        /// Expose the <b>Fluent API</b>, allowing for configuration to be performed without modifying the entity
        /// classes.
        /// </summary>
        /// <param name="onModelCreating">Action to be executed when the model is being constructed.</param>
        /// <returns>This builder</returns>
        public SqlDriverBuilder WithModelBuilder(Action<ModelBuilder> onModelCreating) {
            if (OnModelCreating != default) {
                throw new InvalidOperationException("Only one action may be executed when the model is being built.");
            }
            OnModelCreating = onModelCreating;
            return this;
        }

        public override IDriver Build(bool withDatabaseCreated = false) {
            var driver = Build();
            if (withDatabaseCreated) {
                driver.EnsureDatabaseCreated();
            }
            return driver;
        }

        protected abstract IDriver Build();
    }
}