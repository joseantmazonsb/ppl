using System;

namespace PluggablePersistenceLayer.DynamicProxy.Models {

    /// <summary>
    /// Represents the definition of a dynamic property which can be added to an object at runtime.
    /// </summary>
    public class DynamicProperty {
        /// <summary>
        /// The Name of the property.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// The Display Name of the property for the end-user.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// The underlying System Type of the property.
        /// </summary>
        public Type Type { get; set; }
    }
}