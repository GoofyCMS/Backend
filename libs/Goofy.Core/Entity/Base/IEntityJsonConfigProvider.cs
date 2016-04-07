using System;
using System.Collections.Generic;

namespace Goofy.Core.Entity.Base
{
    public interface IEntityJsonConfigProvider
    {
        IDictionary<string, object> GetConfigurations<TEntity>() where TEntity : class;
        IDictionary<string, object> GetConfigurations(Type entityType);
    }
}
