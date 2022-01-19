using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using PluggablePersistenceLayer.Core;
using PluggablePersistenceLayer.Core.Drivers;

namespace MongoDb {
    public class MongoDbDriver : Driver {
        private readonly IMongoDatabase _database;
        private readonly IClientSession _session;
        private bool _transactionInProgress;

        public override bool SupportsTransactions { get; }
        
        public MongoDbDriver(string connectionString, IEnumerable<Dataset> datasets) : base(datasets) {
            var url = new MongoUrl(connectionString);
            var client = new MongoClient(url);
            _database = client.GetDatabase(url.DatabaseName);
            _session = client.StartSession();
            SupportsTransactions = IsReplicaSetEnabled();
            if (BsonSerializer.LookupSerializer<GuidSerializer>() != null) return;
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        }

        private bool IsReplicaSetEnabled() {
            try {
                _session.StartTransaction();
                _session.AbortTransaction();
                return true;
            }
            catch (NotSupportedException) {
                return false;
            }
        }

        protected override void AssertSupportedType<T>() {}
        
        private IMongoCollection<T> GetCollection<T>() {
            return _database.GetCollection<T>(typeof(T).Name);
        }

        private void StartTransaction() {
            if (!SupportsTransactions || _transactionInProgress) return;
            _session.StartTransaction();
            _transactionInProgress = true;
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
            StartTransaction();
            collection.InsertOne(entity);
            return collection.Find(e => e.Id == entity.Id).Single();
        }

        protected override T DoUpdate<T>(T entity) {
            var collection = GetCollection<T>();
            StartTransaction();
            collection.ReplaceOne(e => e.Id == entity.Id, entity);
            return collection.Find(e => e.Id == entity.Id).Single();
        }

        protected override IDriver DoRemove<T>(T entity) {
            GetCollection<T>().DeleteOne(e => e.Id == entity.Id);
            return this;
        }
        
        protected override void Rollback() {
            if (_transactionInProgress) return;
            _session.AbortTransaction();
            _transactionInProgress = false;
        }

        protected override void Commit() {
            if (_transactionInProgress) return;
            _session.CommitTransaction();
            _transactionInProgress = false;
        }
    }
}