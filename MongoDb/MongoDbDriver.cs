using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using PluggablePersistenceLayer.Core;
using PluggablePersistenceLayer.Core.Builders;
using PluggablePersistenceLayer.Core.Drivers;
using PluggablePersistenceLayer.MongoDb.Builders;

namespace PluggablePersistenceLayer.MongoDb {
    /// <summary>
    /// Wrapper to interact with MongoDb databases.
    /// </summary>
    public class MongoDbDriver : Driver {
        private readonly IMongoDatabase _database;
        private IClientSession _session;
        private IClientSession Session => _session ??= _client.StartSession();
        private readonly MongoDbDriverOptions _options;
        private readonly MongoClient _client;

        public MongoDbDriver(string connectionString, IEnumerable<Dataset> datasets, 
            MongoDbDriverOptions options) : base(connectionString, datasets) {
            _options = options ?? new MongoDbDriverOptions();
            var url = new MongoUrl(connectionString);
            _client = new MongoClient(url);
            _database = _client.GetDatabase(url.DatabaseName);
            if (BsonSerializer.LookupSerializer<GuidSerializer>() != null) return;
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        }

        protected override void AssertSupportedType<T>() {
            if (_options.AllowAnyDataset) return;
            var type = typeof(T);
            if (Datasets.Select(d => d.Type).Contains(type)) return;
            throw new InvalidOperationException($"No dataset available for type '{type}'. " +
                                                $"Register the type using a {nameof(IDriverBuilder)} or" +
                                                $"enable the option {nameof(_options.AllowAnyDataset)} to" +
                                                $"avoid this error.");
        }
        
        private IMongoCollection<T> GetCollection<T>() {
            return _database.GetCollection<T>(typeof(T).Name);
        }

        public override T Get<T>(Guid id) {
            return GetAll<T>().Single(e => e.Id == id);
        }

        public override IEnumerable<T> GetAll<T>() {
            return GetCollection<T>()
                .AsQueryable()
                .ToList();
        }

        protected override T DoInsert<T>(T entity) {
            var collection = GetCollection<T>();
            collection.InsertOne(entity);
            return collection.Find(e => e.Id == entity.Id).Single();
        }

        protected override T DoUpdate<T>(T entity) {
            var collection = GetCollection<T>();
            collection.ReplaceOne(e => e.Id == entity.Id, entity);
            return collection.Find(e => e.Id == entity.Id).Single();
        }

        protected override IDriver DoRemove<T>(T entity) {
            GetCollection<T>().DeleteOne(e => e.Id == entity.Id);
            return this;
        }

        public override void SaveChanges() {
            if (_options.Pedantic) throw new NotSupportedException("Unlike with other providers, " +
                                                                   "this method won't actually do anything.");
        }

        public override void BeginTransaction() {
            try {
                Session.StartTransaction();
            }
            catch (NotSupportedException) {
                if (_options.FailIfTransactionsNotSupported) throw;
            }
        }

        public override void CommitTransaction() {
            try {
                using (Session) {
                    Session.CommitTransaction();
                }
                _session = default;
            }
            catch (NotSupportedException) {
                if (_options.FailIfTransactionsNotSupported) throw;
            }
        }

        public override void RollbackTransaction() {
            try {
                using (Session) {
                    Session.AbortTransaction();
                }
                _session = default;
            }
            catch (NotSupportedException) {
                if (_options.FailIfTransactionsNotSupported) throw;
            }
        }
    }
}