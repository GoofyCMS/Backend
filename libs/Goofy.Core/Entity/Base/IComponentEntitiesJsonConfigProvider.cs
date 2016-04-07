using System;
using System.Collections.Generic;

namespace Goofy.Core.Entity.Base
{
    public interface IComponentEntitiesJsonConfigProvider
    {
        KeyValuePair<string, object> ConfigureComponentEntities(IEnumerable<Type> typesInfo);
    }
}
