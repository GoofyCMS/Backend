using System;
using System.Collections.Generic;
using Goofy.Core.Components.Base;
using Goofy.Core.Entity.Base;

namespace Goofy.Core.WebFramework.Components
{
    public class ComponentJsonConfigProvider : IComponentJsonConfigProvider
    {
        private readonly IComponentEntitiesJsonConfigProvider _componentEntitiesJsonConfigProvider;

        public ComponentJsonConfigProvider(IComponentEntitiesJsonConfigProvider componentEntitiesJsonConfigProvider)
        {
            _componentEntitiesJsonConfigProvider = componentEntitiesJsonConfigProvider;
        }

        public IDictionary<string, object> GetComponentJsonConfiguration(IEnumerable<Type> entityTypes)
        {
            var entitiesConfiguration = _componentEntitiesJsonConfigProvider.ConfigureComponentEntities(entityTypes);
            var result = new Dictionary<string, object>() {
                            {entitiesConfiguration.Key, entitiesConfiguration.Value }
                         };
            return result;

        }
    }
}
