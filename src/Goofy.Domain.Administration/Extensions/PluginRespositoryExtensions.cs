using Goofy.Domain.Administration.Entity;
using Goofy.Domain.Core.Service.Data;
using System.Linq;

namespace Goofy.Domain.Administration.Extensions
{
    public static class PluginRespositoryExtensions
    {
        public static Plugin GetPluginByName(this IReadOnlyRepository<Plugin> plugins, string pluginName)
        {
            return plugins.GetAll(p => p.Name == pluginName).FirstOrDefault();
        }

        public static Plugin GetPluginById(this IReadOnlyRepository<Plugin> plugins, int pluginId)
        {
            return plugins.Find(pluginId);
        }
    }
}
