using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Reflection.Emit;
using DynamicProxy.Extensions;
using DynamicProxy.Models;

namespace DynamicProxy.Factories {
    /// <summary>
    /// Generates new Types with dynamically added properties.
    /// </summary>
    public class DynamicTypeFactory {
        private TypeBuilder _typeBuilder;
        private readonly ModuleBuilder _moduleBuilder;

        public const string DynamicAssemblyName = "DynamicTypes";
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public DynamicTypeFactory() {
            var assemblyName = new AssemblyName(DynamicAssemblyName);
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndCollect);
            _moduleBuilder = assemblyBuilder.DefineDynamicModule(Guid.NewGuid().ToString());
        }

        #region Methods

        #region Public

        /// <summary>
        /// Creates a new Type based on the specified parent Type and attaches dynamic properties.
        /// </summary>
        /// <param name="parentType">The parent Type to base the new Type on</param>
        /// <param name="dynamicProperties">The collection of dynamic properties to attach to the new Type</param>
        /// <returns>An extended Type with dynamic properties added to it</returns>
        public Type CreateNewTypeWithDynamicProperties(Type parentType,
            IEnumerable<DynamicProperty> dynamicProperties) {
            _typeBuilder = _moduleBuilder.DefineType(parentType.Name + Guid.NewGuid(), TypeAttributes.Public);
            _typeBuilder.SetParent(parentType);

            foreach (var property in dynamicProperties)
                AddDynamicPropertyToType(property);
            return _typeBuilder.CreateType();
        }

        #endregion

        #region Private

        /// <summary>
        /// Adds the specified dynamic property to the new type.
        /// </summary>
        /// <param name="dynamicProperty">The definition of the dynamic property to add to the Type</param>
        private void AddDynamicPropertyToType(DynamicProperty dynamicProperty) {
            var propertyType = dynamicProperty.Type;
            var propertyName = $"{dynamicProperty.PropertyName}";
            var fieldName = $"_{propertyName.ToCamelCase()}";

            var fieldBuilder = _typeBuilder.DefineField(fieldName, propertyType, FieldAttributes.Private);

            // The property set and get methods require a special set of attributes.
            var getSetAttributes = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;

            // Define the 'get' accessor method.
            var getMethodBuilder =
                _typeBuilder.DefineMethod($"get_{propertyName}", getSetAttributes, propertyType, Type.EmptyTypes);
            var propertyGetGenerator = getMethodBuilder.GetILGenerator();
            propertyGetGenerator.Emit(OpCodes.Ldarg_0);
            propertyGetGenerator.Emit(OpCodes.Ldfld, fieldBuilder);
            propertyGetGenerator.Emit(OpCodes.Ret);

            // Define the 'set' accessor method.
            var setMethodBuilder = _typeBuilder.DefineMethod($"set_{propertyName}", getSetAttributes, null,
                new Type[] { propertyType });
            var propertySetGenerator = setMethodBuilder.GetILGenerator();
            propertySetGenerator.Emit(OpCodes.Ldarg_0);
            propertySetGenerator.Emit(OpCodes.Ldarg_1);
            propertySetGenerator.Emit(OpCodes.Stfld, fieldBuilder);
            propertySetGenerator.Emit(OpCodes.Ret);

            // Lastly, we must map the two methods created above to a PropertyBuilder and their corresponding behaviors, 'get' and 'set' respectively.
            var propertyBuilder =
                _typeBuilder.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);
            propertyBuilder.SetGetMethod(getMethodBuilder);
            propertyBuilder.SetSetMethod(setMethodBuilder);

            // Add a 'DisplayName' attribute.
            var attributeType = typeof(DisplayNameAttribute);
            var attributeBuilder = new CustomAttributeBuilder(
                attributeType.GetConstructor(new[] { typeof(string) }), // Constructor selection.
                new object[] { dynamicProperty.DisplayName }, // Constructor arguments.
                Array.Empty<PropertyInfo>(), // Properties to assign to.                    
                Array.Empty<object>() // Values for property assignment.
            );
            propertyBuilder.SetCustomAttribute(attributeBuilder);
        }

        #endregion

        #endregion
    }
}