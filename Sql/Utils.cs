using System;
using System.Collections.Generic;
using DynamicProxy.Factories;
using DynamicProxy.Models;
using Microsoft.EntityFrameworkCore;

namespace Sql {
    public static class Utils {
        public static T CreateDbContext<T>(IEnumerable<Type> sets) where T : DbContext {
            var properties = new List<DynamicProperty>();
            foreach (var type in sets) {
                var genericType = typeof(DbSet<>).MakeGenericType(type);
                properties.Add(new DynamicProperty {
                    DisplayName = type.Name,
                    PropertyName = type.Name,
                    Type = genericType
                });
            }
            var t = new DynamicTypeFactory().CreateNewTypeWithDynamicProperties(typeof(T), properties);
            var context = Activator.CreateInstance(t) as T;
            return context;
        }
    }
}