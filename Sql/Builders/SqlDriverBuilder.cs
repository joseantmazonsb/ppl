using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PluggablePersistenceLayer.Core.Builders;
using PluggablePersistenceLayer.Core.Drivers;

namespace PluggablePersistenceLayer.Sql.Builders {
    public abstract class SqlDriverBuilder : DriverBuilder {
        protected Action<IRelationalDbContextOptionsBuilderInfrastructure> Options;

        protected SqlDriverBuilder(string connectionString) : base(connectionString) {
        }

        /// <summary>
        /// Allows relational database specific configuration to be performed on DbContextOptions.
        /// </summary>
        /// <param name="options">Provider specific options.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IDriverBuilder WithOptions<T>(Action<T> options)
            where T : IRelationalDbContextOptionsBuilderInfrastructure {
            Options = o => options((T) o);
            return this;
        }

        public override IDriver Build() {
            var context = CreateContext();
            context.ConnectionString = ConnectionString;
            return new SqlDriver(context, Datasets);
        }

        protected abstract SqlContext CreateContext();
    }
}