using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Exceptions;

namespace Core.Drivers {
    public interface IDriver {
        /// <summary>
        /// Retrieve all entities which match the given filter from the database. 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IEnumerable<T> GetAll<T>(Func<T, bool> filter) where T : Entity;
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
        /// <exception cref="EntityAlreadyExistsError">If the entity already exists.</exception>
        T Insert<T>(T entity)  where T : Entity;
        /// <summary>
        /// Update an already existent entity in the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>The updated user.</returns>
        /// <exception cref="EntityNotFoundError">If the entity does not exist.</exception>
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
        bool Exists<T>(Func<T, bool> filter) where T : Entity;
        /// <summary>
        /// Delete the given entity from the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="EntityNotFoundError">If the entity does not exist.</exception>
        IDriver Remove<T>(T entity) where T : Entity;
        /// <summary>
        /// Delete all entities which match the given filter from the database.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>The removed entities.</returns>
        IEnumerable<T> Remove<T>(Func<T, bool> filter) where T : Entity;
        /// <summary>
        /// Delete an entity given its identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The removed entity.</returns>
        /// <exception cref="EntityNotFoundError">If the entity does not exist.</exception>
        T Remove<T>(Guid id) where T : Entity;
        /// <summary>
        /// Delete all entities of the specified type.
        /// </summary>
        /// <returns></returns>
        void RemoveAll<T>() where T : Entity;
        /// <summary>
        /// Save all changes made to the database.
        /// </summary>
        void SaveChanges();
        /// <summary>
        /// Discard all changes made to the database.
        /// </summary>
        void DiscardChanges();
    }
}