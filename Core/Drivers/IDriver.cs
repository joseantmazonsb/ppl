using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PluggablePersistenceLayer.Core.Drivers {
    public interface IDriver {
        void EnsureDatabaseCreated();
        void EnsureDatabaseDeleted();
        /// <summary>
        /// Datasets managed by the driver.
        /// </summary>
        IEnumerable<Dataset> Datasets { get; }
        /// <summary>
        /// Retrieve all entities which match the given filter from the database. 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IEnumerable<T> GetAll<T>(Expression<Func<T, bool>> filter) where T : Entity;
        /// <summary>
        /// Retrieve the only entity for the provided id (if any). 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get<T>(Guid id)  where T : Entity;
        /// <summary>
        /// Retrieve all entities from the database.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll<T>()  where T : Entity;
        /// <summary>
        /// Insert an entity into the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>The inserted entity.</returns>
        T Insert<T>(T entity)  where T : Entity;
        /// <summary>
        /// Update an already existent entity in the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>The updated user.</returns>
        T Update<T>(T entity)  where T : Entity;
        /// <summary>
        /// Check whether an entity exists in the database or not.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>True if the entity exists. False otherwise.</returns>
        bool Exists<T>(T entity) where T : Entity;
        /// <summary>
        /// Check whether one or more entities matching the given filter exist in the database or not.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        bool Exists<T>(Expression<Func<T, bool>> filter) where T : Entity;
        /// <summary>
        /// Delete the given entity from the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        IDriver Remove<T>(T entity) where T : Entity;
        /// <summary>
        /// Delete all entities which match the given filter from the database.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>The removed entities.</returns>
        IEnumerable<T> Remove<T>(Expression<Func<T, bool>> filter) where T : Entity;
        /// <summary>
        /// Delete an entity given its identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The removed entity.</returns>
        T Remove<T>(Guid id) where T : Entity;
        /// <summary>
        /// Delete all entities of the specified type.
        /// </summary>
        /// <returns></returns>
        void RemoveAll<T>() where T : Entity;
        /// <summary>
        /// Save all changes made to the database.
        /// </summary>
        /// <remarks>Will throw an exception if the driver does not support transactions.</remarks>
        void SaveChanges();
        /// <summary>
        /// Start a new transaction.
        /// </summary>
        void BeginTransaction();
        /// <summary>
        /// Commit and dispose the current transaction.
        /// </summary>
        void CommitTransaction();
        /// <summary>
        /// Rollback and dispose the current transaction.
        /// </summary>
        void RollbackTransaction();
    }
}