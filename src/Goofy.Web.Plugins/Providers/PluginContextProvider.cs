using Goofy.Domain.Core.Service.Adapter;
using Goofy.Infrastructure.Plugins.Data;
using Goofy.Web.Core.Providers;

namespace Goofy.Web.Plugins.Providers
{
    public class PluginContextProvider : BaseContextProvider<PluginMetadataContext>
    {
        public PluginContextProvider(ITypeAdapterFactory typeAdapterFactory)
            /* TODO: Fix this... */
            : base(new PluginsContext(@"Data Source=LEO_PC\SQLEXPRESS;Initial Catalog=goofy_database;Integrated Security=True"), typeAdapterFactory)
        {
        }
    }
}
