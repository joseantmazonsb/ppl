using System;
using System.Collections.Generic;
using System.Linq;
using Core.Drivers;
using Core.Entities;

namespace Sql.Builders {
    public abstract class SqlDriverBuilder : ISqlDriverBuilder {

        protected readonly HashSet<Type> Sets;
        private readonly string _connectionString;

        protected SqlDriverBuilder(string connectionString) {
            _connectionString = connectionString;
            Sets = new HashSet<Type>();
        }

        public ISqlDriverBuilder WithSet<T>() where T : Entity {
            Sets.Add(typeof(T));
            return this;
        }

        public IDriver Build() {
            var errorTitle = $"Cannot build {nameof(SqlDriver)} instance:";
            if (string.IsNullOrEmpty(_connectionString)) {
                throw new InvalidOperationException($"{errorTitle} the connection string cannot be null nor empty!");
            }
            if (!Sets.Any()) {
                throw new InvalidOperationException($"{errorTitle} there are no datasets! " +
                                                    $"Use method '{nameof(WithSet)}' to set the datasets.");
            }
            var context = CreateContext();
            context.ConnectionString = _connectionString;
            return new SqlDriver(context);
        }

        protected abstract SqlContext CreateContext();
    }
}