using System;
using System.Reflection;
using System.Collections.Generic;
using Goofy.Core.Entity.Base;
using System.Linq;

namespace Goofy.Core.WebFramework.Components
{
    public class EntityJsonConfigProvider : IEntityJsonConfigProvider
    {
        private readonly IEntityPropertyAttributeConfigProvider _propertyAttributeConfigProvider;

        public EntityJsonConfigProvider(IEntityPropertyAttributeConfigProvider propertyAttributeConfigProvider)
        {
            _propertyAttributeConfigProvider = propertyAttributeConfigProvider;
        }

        public IDictionary<string, object> GetConfigurations(Type entityType)
        {
            var result = new Dictionary<string, object>();
            var typeInfo = entityType.GetTypeInfo();
            foreach (var propertyInfo in typeInfo.DeclaredProperties)
            {
                var fieldConfig = new Dictionary<string, object>();
                result.Add(propertyInfo.Name, fieldConfig);
                foreach (var attributeData in propertyInfo.CustomAttributes)
                {
                    var arguments = attributeData.ConstructorArguments.Select(arg => arg.Value)
                                                 .ToArray();
                    /*
                        TODO:
                        Buscar forma equivalente de ejecutar este código, equivalente con el
                        netcore, la solución podría ser usar el "type" del Attributo en lugar de
                        crear la instancia y además pasar el los NamedArguments
                    */
                    //var attribute = (Attribute)Activator.CreateInstance(attributeData.GetType(), arguments);
                    //var attribute = (Attribute)attributeData.Constructor.Invoke(arguments);
                    //var attrConfig = _propertyAttributeConfigProvider.GetConfig(attribute);
                    //if (attrConfig.Value is List<string>)
                    //{
                    //    if (fieldConfig.ContainsKey(attrConfig.Key))
                    //        ((List<string>)fieldConfig[attrConfig.Key]).AddRange((IEnumerable<string>)attrConfig.Value);
                    //    else
                    //        fieldConfig.Add(attrConfig.Key, attrConfig.Value);
                    //}
                    //else
                    //{
                    //    fieldConfig.Add(attrConfig.Key, attrConfig.Value);
                    //}
                }
            }
            return result;
        }

        public IDictionary<string, object> GetConfigurations<TEntity>() where TEntity : class
        {
            return GetConfigurations(typeof(TEntity));
        }
    }
}
