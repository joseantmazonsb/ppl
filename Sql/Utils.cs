using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PluggablePersistenceLayer.Core;
using PluggablePersistenceLayer.DynamicProxy.Factories;
using PluggablePersistenceLayer.DynamicProxy.Models;

namespace PluggablePersistenceLayer.Sql {
    public static class Utils {
        public static T CreateDbContext<T>(IEnumerable<Dataset> datasets) where T : DbContext {
            var properties = new List<DynamicProperty>();
            foreach (var dataset in datasets) {
                var genericType = typeof(DbSet<>).MakeGenericType(dataset.Type);
                properties.Add(new DynamicProperty {
                    DisplayName = dataset.Name,
                    PropertyName = dataset.Name,
                    Type = genericType
                });
            }
            var t = new DynamicTypeFactory().CreateNewTypeWithDynamicProperties(typeof(T), properties);
            var context = Activator.CreateInstance(t) as T;
            return context;
        }
    }
}