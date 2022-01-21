using PluggablePersistenceLayer.Core.Drivers;

namespace PluggablePersistenceLayer.Core.Builders {
    public interface IDriverBuilder {
        /// <summary>
        /// Build a new persistence layer driver.
        /// </summary>
        /// <returns></returns>
        IDriver Build(bool withDatabaseCreated = false);
        /// <summary>
        /// Add a dataset to the driver in construction.
        /// </summary>
        /// <typeparam name="T">Type of the data to be stored.
        /// Will be used to determine the name of the dataset.</typeparam>
        /// <returns>This builder</returns>
        IDriverBuilder WithDataset<T>() where T : Entity;
        /// <summary>
        /// Add a dataset to the driver in construction.
        /// </summary>
        /// <param name="name">Name of the dataset.</param>
        /// <typeparam name="T">Type of the data to be stored.</typeparam>
        /// <returns>This builder</returns>
        IDriverBuilder WithDataset<T>(string name) where T : Entity;
    }
}