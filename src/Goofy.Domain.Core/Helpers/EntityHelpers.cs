using System.Linq;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Goofy.Domain.Core
{
    public static class EntityHelpers
    {
        public static object[] GetEntityKeys(this object entity)
        {
            return entity.GetEntityKeyProperties().Select(e => e.GetValue(entity)).ToArray();
        }

        public static IEnumerable<PropertyInfo> GetEntityKeyProperties(this object entity)
        {
            var keyProperties = entity.GetType()
                .GetProperties()
                .Where(e => e.GetCustomAttributes().Any(a => a is KeyAttribute));
            return keyProperties;
        }
    }
}
