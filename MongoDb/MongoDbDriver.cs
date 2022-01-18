using System;
using System.Collections.Generic;
using System.Linq;
using Core.Drivers;
using Core.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace MongoDb {
    public class MongoDbDriver : IDriver {

        private readonly IMongoDatabase _database;
        private readonly IClientSession _session;
        private bool _transactionInProgress;

        public MongoDbDriver(string connectionString) {
            var url = new MongoUrl(connectionString);
            var client = new MongoClient(url);
            _database = client.GetDatabase(url.DatabaseName);
            _session = client.StartSession();
            if (BsonSerializer.LookupSerializer<GuidSerializer>() != null) return;
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        }

        private IMongoCollection<T> GetCollection<T>() {
            return _database.GetCollection<T>(typeof(T).Name);
        }

        private void StartTransaction() {
            if (_transactionInProgress) return;
            _session.StartTransaction();
            _transactionInProgress = true;
        }
        
        private void CommitTransaction() {
            if (!_transactionInProgress) return;
            _session.CommitTransaction();
            _transactionInProgress = false;
        }
        
        private void AbortTransaction() {
            if (!_transactionInProgress) return;
            _session.AbortTransaction();
            _transactionInProgress = false;
        }

        public IEnumerable<T> GetAll<T>(Func<T, bool> filter) where T : Entity {
            return GetAll<T>().Where(filter);
        }

        public T Get<T>(Guid id) where T : Entity {
            return GetAll<T>().Single(e => e.Id == id);
        }

        public IEnumerable<T> GetAll<T>() where T : Entity {
            return GetCollection<T>()
                .AsQueryable()
                .AsEnumerable();
        }

        public T Insert<T>(T entity) where T : Entity {
            var collection = GetCollection<T>();
            StartTransaction();
            collection.InsertOne(entity);
            return collection.Find(e => e.Id == entity.Id).Single();
        }

        public T Update<T>(T entity) where T : Entity {
            var collection = GetCollection<T>();
            StartTransaction();
            collection.ReplaceOne(e => e.Id == entity.Id, entity);
            return collection.Find(e => e.Id == entity.Id).Single();
        }

        public bool Exists<T>(T entity) where T : Entity {
            return GetAll<T>().SingleOrDefault(e => e.Id == entity.Id) != null;
        }

        public bool Exists<T>(Func<T, bool> filter) where T : Entity {
            return GetAll(filter).Any();
        }

        public IDriver Remove<T>(T entity) where T : Entity {
            Remove<T>(entity.Id);
            return this;
        }

        public IEnumerable<T> Remove<T>(Func<T, bool> filter) where T : Entity {
            var remove = GetAll(filter);
            var collection = GetCollection<T>();
            StartTransaction();
            collection.DeleteMany(f => filter(f));
            return remove;
        }

        public T Remove<T>(Guid id) where T : Entity {
            return Remove<T>(e => e.Id == id).Single();
        }

        public void RemoveAll<T>() where T : Entity {
            Remove<T>(e => true);
        }

        public void SaveChanges() {
            CommitTransaction();
        }

        public void DiscardChanges() {
            /*
            if (string.IsNullOrEmpty(_url.ReplicaSetName)) {
                throw new NoReplicaSetError("If no replica set is configured, all operations performed are final.");
            }
            */
            AbortTransaction();
        }
    }
}