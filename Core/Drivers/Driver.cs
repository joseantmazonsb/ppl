using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PluggablePersistenceLayer.Core.Builders;

namespace PluggablePersistenceLayer.Core.Drivers {
    public abstract class Driver : IDriver {
        public IEnumerable<Dataset> Datasets { get; }

        protected Driver(IEnumerable<Dataset> datasets) {
            Datasets = datasets;
        }
        
        protected virtual void AssertSupportedType<T>() where T : Entity {
            var type = typeof(T);
            if (!Datasets.Select(s => s.Type).Contains(type)) {
                throw new InvalidOperationException($"No dataset available for type '{type}'. " +
                                                    $"Register the type using a {nameof(IDriverBuilder)}.");
            }
        }
        
        public IEnumerable<T> GetAll<T>(Expression<Func<T, bool>> filter) where T : Entity {
            AssertSupportedType<T>();
            return GetAll<T>().Where(filter.Compile());
        }

        public abstract IEnumerable<T> GetAll<T>() where T : Entity;
        public abstract T Get<T>(Guid id) where T : Entity;
        
        public bool Exists<T>(T entity) where T : Entity {
            return GetAll<T>(e => e.Id == entity.Id).SingleOrDefault() != null;
        }

        public bool Exists<T>(Expression<Func<T, bool>> filter) where T : Entity {
            return GetAll(filter).Any();
        }

        public T Insert<T>(T entity) where T : Entity {
            AssertSupportedType<T>();
            return DoInsert(entity);
        }

        public T Update<T>(T entity) where T : Entity {
            AssertSupportedType<T>();
            return DoUpdate(entity);
        }

        protected abstract T DoInsert<T>(T entity) where T : Entity;
        protected abstract T DoUpdate<T>(T entity) where T : Entity;
        
        public IEnumerable<T> Remove<T>(Expression<Func<T, bool>> filter) where T : Entity {
            AssertSupportedType<T>();
            var removed = new List<T>();
            foreach (var entity in GetAll(filter)) {
                try {
                    Remove(entity);
                    removed.Add(entity);
                }
                catch (Exception) {
                    // Ignore
                }
            }
            return removed;
        }

        public IDriver Remove<T>(T entity) where T : Entity {
            AssertSupportedType<T>();
            DoRemove(entity);
            return this;
        }
        protected abstract IDriver DoRemove<T>(T entity) where T : Entity;
        public T Remove<T>(Guid id) where T : Entity {
            return Remove<T>(e => e.Id == id).Single();
        }

        public void RemoveAll<T>() where T : Entity {
            Remove<T>(e => true);
        }

        public abstract void SaveChanges();
        public abstract void BeginTransaction();
        public abstract void CommitTransaction();
        public abstract void RollbackTransaction();
    }
}