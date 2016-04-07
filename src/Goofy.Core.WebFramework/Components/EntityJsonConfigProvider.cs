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
            foreach (var propertyInfo in typeInfo.GetProperties())
            {
                var fieldConfig = new Dictionary<string, object>();
                result.Add(propertyInfo.Name, fieldConfig);
                foreach (var attributeData in propertyInfo.GetCustomAttributesData())
                {
                    var arguments = attributeData.ConstructorArguments.Select(arg => arg.Value)
                                                 .ToArray();
                    var attribute = (Attribute)attributeData.Constructor.Invoke(arguments);
                    var attrConfig = _propertyAttributeConfigProvider.GetConfig(attribute);
                    if (attrConfig.Value is List<string>)
                    {
                        if (fieldConfig.ContainsKey(attrConfig.Key))
                            ((List<string>)fieldConfig[attrConfig.Key]).AddRange((IEnumerable<string>)attrConfig.Value);
                        else
                            fieldConfig.Add(attrConfig.Key, attrConfig.Value);
                    }
                    else
                    {
                        fieldConfig.Add(attrConfig.Key, attrConfig.Value);
                    }
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
