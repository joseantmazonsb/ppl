using System;
using System.Collections.Generic;
using PluggablePersistenceLayer.Core;
using PluggablePersistenceLayer.Core.Drivers;

namespace PluggablePersistenceLayer.Sql {
    public class SqlDriver : Driver {
    
        private readonly SqlContext _context;
    
        public SqlDriver(SqlContext context, IEnumerable<Dataset> datasets) : base(datasets) {
            _context = context;
            _context.Database.EnsureCreated();
        }

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

        public override void SaveChanges() {
            _context.SaveChanges();
        }

        public override void BeginTransaction() {
            _context.Database.BeginTransaction();
        }

        public override void CommitTransaction() {
            _context.Database.CommitTransaction();
        }

        public override void RollbackTransaction() {
            _context.Database.RollbackTransaction();
        }
    }
}