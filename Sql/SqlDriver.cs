using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PluggablePersistenceLayer.Core;
using PluggablePersistenceLayer.Core.Drivers;

namespace PluggablePersistenceLayer.Sql {
    public abstract class SqlDriver : Driver {
    
        private SqlContext _context;
        private readonly Action<IRelationalDbContextOptionsBuilderInfrastructure> _options;

        private SqlContext Context => _context ??= InitializeContext();

        private SqlContext InitializeContext() {
            var ctx = CreateContext();
            ctx.Options = _options;
            ctx.ConnectionString = ConnectionString;
            ctx.Database.EnsureCreated();
            return ctx;
        }
        protected abstract SqlContext CreateContext();

        protected SqlDriver(string connectionString, IEnumerable<Dataset> datasets, 
            Action<IRelationalDbContextOptionsBuilderInfrastructure> options) : base(connectionString, datasets) {
            _options = options;
        }

        public override IEnumerable<T> GetAll<T>() {
            return Context.Set<T>();
        }
        public override T Get<T>(Guid id) {
            return Context.Find<T>(id);
        }

        protected override T DoInsert<T>(T entity) {
            return Context.Add(entity).Entity;
        }

        protected override T DoUpdate<T>(T entity) {
            return Context.Update(entity).Entity;
        }

        protected override IDriver DoRemove<T>(T entity) {
            Context.Attach(entity); // TODO needed?
            Context.Remove(entity);
            return this;
        }

        public override void SaveChanges() {
            Context.SaveChanges();
        }

        public override void BeginTransaction() {
            Context.Database.BeginTransaction();
        }

        public override void CommitTransaction() {
            using (Context) {
                Context.Database.CommitTransaction();
            }
            _context = default;
        }

        public override void RollbackTransaction() {
            using (Context) {
                Context.Database.RollbackTransaction();
            }
            _context = default;
        }
    }
}