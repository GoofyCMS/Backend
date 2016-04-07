using System;
using System.Collections.Generic;

namespace Goofy.Core.Entity.Base
{
    public interface IEntityPropertyAttributeConfigProvider
    {
        KeyValuePair<string, object> GetConfig(Attribute propertyAttribute);
    }
}
