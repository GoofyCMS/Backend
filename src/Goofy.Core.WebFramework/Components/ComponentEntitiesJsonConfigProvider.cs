using System;
using System.Collections.Generic;
using Goofy.Core.Entity.Base;

namespace Goofy.Core.WebFramework.Components
{
    public class ComponentEntitiesJsonConfigProvider : IComponentEntitiesJsonConfigProvider
    {
        private readonly IEntityJsonConfigProvider _entityJsonConfigProvider;

        public ComponentEntitiesJsonConfigProvider(IEntityJsonConfigProvider entityJsonConfigProvider)
        {
            _entityJsonConfigProvider = entityJsonConfigProvider;
        }

        public KeyValuePair<string, object> ConfigureComponentEntities(IEnumerable<Type> entityTypes)
        {
            var entitiesConfig = new Dictionary<string, object>();
            foreach (var type in entityTypes)
            {
                var entityConfiguration = _entityJsonConfigProvider.GetConfigurations(type);
                entitiesConfig.Add(type.Name, entityConfiguration);
            }
            return new KeyValuePair<string, object>("entities", entitiesConfig); ;
        }
    }
}
