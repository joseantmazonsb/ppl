using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PluggablePersistenceLayer.Core;
using PluggablePersistenceLayer.Core.Drivers;

namespace Sql {
    public class SqlDriver : Driver {
    
        private readonly SqlContext _context;
    
        public SqlDriver(SqlContext context, IEnumerable<Dataset> datasets) : base(datasets) {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public override bool SupportsTransactions => true;

        public override IEnumerable<T> GetAll<T>() {
            return _context.Set<T>();
        }
        public override T Get<T>(Guid id) {
            return _context.Find<T>(id);
        }

        protected override T DoInsert<T>(T entity) {
            return _context.Add(entity).Entity;
        }

        protected override T DoUpdate<T>(T entity) {
            return _context.Update(entity).Entity;
        }

        protected override IDriver DoRemove<T>(T entity) {
            _context.Attach(entity); // TODO needed?
            _context.Remove(entity);
            return this;
        }
        
        protected override void Commit() {
            _context.SaveChanges();
        }
        
        protected override void Rollback() {
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