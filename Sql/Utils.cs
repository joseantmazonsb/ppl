using System;
using System.Collections.Generic;
using DynamicProxy.Factories;
using DynamicProxy.Models;
using Microsoft.EntityFrameworkCore;
using PluggablePersistenceLayer.Core;

namespace Sql {
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