using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Exceptions;

namespace Core.Drivers {
    public abstract class Driver : IDriver {
        public IEnumerable<T> GetAll<T>(Func<T, bool> filter) where T : Entity {
            return GetAll<T>().Where(filter);
        }
        public IEnumerable<T> GetAll<T>() where T : Entity {
            return DoGetAll<T>();
        }
        protected abstract IEnumerable<T> DoGetAll<T>() where T : Entity;

        public T Get<T>(Guid id) where T : Entity {
            return DoGet<T>(id);
        }
        protected abstract T DoGet<T>(Guid id) where T : Entity;
        
        public bool Exists<T>(T entity) where T : Entity {
            return GetAll<T>(e => e.Id == entity.Id).SingleOrDefault() != null;
        }

        public bool Exists<T>(Func<T, bool> filter) where T : Entity {
            return GetAll(filter).Any();
        }
        
        public T Insert<T>(T entity) where T : Entity {
            if (Exists(entity)) throw new EntityAlreadyExistsError();
            try {
                return DoInsert(entity);
            }
            catch (Exception e) {
                throw new EntityNotInsertedError(e);
            }
        }
        protected abstract T DoInsert<T>(T entity) where T : Entity;
        
        public T Update<T>(T entity) where T : Entity {
            if (!Exists(entity)) throw new EntityNotFoundError();
            try {
                return DoUpdate(entity);
            }
            catch (Exception e) {
                throw new EntityNotUpdatedError(e);
            }
        }
        protected abstract T DoUpdate<T>(T entity) where T : Entity;
        
        public IEnumerable<T> Remove<T>(Func<T, bool> filter) where T : Entity {
            var removed = new List<T>();
            foreach (var entity in GetAll(filter)) {
                try {
                    DoRemove(entity);
                    removed.Add(entity);
                }
                catch (EntityNotRemovedError) {
                    // Ignore
                }
            }
            return removed;
        }
        public IDriver Remove<T>(T entity) where T : Entity {
            Remove<T>(entity.Id);
            return this;
        }
        public T Remove<T>(Guid id) where T : Entity {
            try {
                return Remove<T>(e => e.Id == id).Single();
            }
            catch (Exception e) {
                throw new EntityNotRemovedError(e);
            }
        }

        public void RemoveAll<T>() where T : Entity {
            Remove<T>(e => true);
        }

        protected abstract T DoRemove<T>(T entity) where T : Entity;

        public abstract void SaveChanges();
        public abstract void DiscardChanges();
    }
}