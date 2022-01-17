using System;
using System.Collections.Generic;
using System.Linq;
using Core.Builders;
using Core.Drivers;
using Core.Entities;
using DynamicProxy.Factories;
using DynamicProxy.Models;
using Microsoft.EntityFrameworkCore;

namespace Sql.Builders {
    public class SqlDriverBuilder : ISqlDriverBuilder {

        private readonly HashSet<Type> _sets;
        private readonly string _connectionString;
        private SqlDriverBuilderOptions _options;

        public SqlDriverBuilder(string connectionString) {
            _connectionString = connectionString;
            _sets = new HashSet<Type>();
        }

        public IDriverBuilder WithOptions(IDriverBuilderOptions options) {
            _options = (SqlDriverBuilderOptions) options;
            return this;
        }

        public ISqlDriverBuilder WithSet<T>() where T : Entity {
            _sets.Add(typeof(T));
            return this;
        }

        public IDriver Build() {
            var errorTitle = $"Cannot build {nameof(SqlDriver)} instance:";
            if (string.IsNullOrEmpty(_connectionString)) {
                throw new InvalidOperationException($"{errorTitle} the connection string cannot be null nor empty!");
            }
            if (!_sets.Any()) {
                throw new InvalidOperationException($"{errorTitle} there are no datasets! " +
                                                    $"Use method '{nameof(WithSet)}' to set the datasets.");
            }
            if (_options is not SqlDriverBuilderOptions) {
                throw new InvalidOperationException($"{errorTitle} you need to provide a valid " +
                                                    $"{nameof(SqlDriverBuilderOptions)} through the method " +
                                                    $"'{nameof(WithOptions)}'.");
            }
            return new SqlDriver(CreateContext());
        }

        private DbContext CreateContext() {
            var properties = new List<DynamicProperty>();
            foreach (var type in _sets) {
                var genericType = typeof(DbSet<>).MakeGenericType(type);
                properties.Add(new DynamicProperty {
                    DisplayName = type.Name,
                    PropertyName = type.Name,
                    Type = genericType
                });
            }
            var t = new DynamicTypeFactory().CreateNewTypeWithDynamicProperties(typeof(SqlContext), properties);
            var context = Activator.CreateInstance(t) as SqlContext;
            context.ConnectionString = _connectionString;
            context.Provider = _options.Provider;
            return context;
        }
    }
}