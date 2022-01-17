using System;
using System.Collections.Generic;
using System.Linq;
using Core.Drivers;
using Microsoft.EntityFrameworkCore;

namespace Sql {
    public class SqlDriver : Driver {
    
        private readonly DbContext _context;
    
        public SqlDriver(DbContext context) {
            _context = context;
            _context.Database.EnsureCreated();
        }
        protected override IEnumerable<T> DoGetAll<T>() {
            return _context.Set<T>();
        }
        protected override T DoGet<T>(Guid id) {
            return _context.Find<T>(id);
        }

        protected override T DoInsert<T>(T entity) {
            return _context.Add(entity).Entity;
        }

        protected override T DoUpdate<T>(T entity) {
            return _context.Update(entity).Entity;
        }

        protected override T DoRemove<T>(T entity) {
            _context.Attach(entity); // TODO needed?
            return _context.Remove(entity).Entity;
        }
        
        public override void SaveChanges() {
            _context.SaveChanges();
        }
        
        public override void DiscardChanges() {
            foreach (var entry in _context.ChangeTracker.Entries().ToList()) {
                switch (entry.State) {
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified; // Revert changes made to deleted entity.
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }
    }
}