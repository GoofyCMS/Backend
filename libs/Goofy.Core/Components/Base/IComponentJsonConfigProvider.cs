using System;
using System.Collections.Generic;

namespace Goofy.Core.Components.Base
{
    public interface IComponentJsonConfigProvider
    {
        IDictionary<string, object> GetComponentJsonConfiguration(IEnumerable<Type> entityTypes);
    }
}
