using Goofy.Domain.Core.Service.Adapter;
using Goofy.Infrastructure.Plugins.Data;
using Goofy.Web.Core.Providers;
using System;

namespace Goofy.Web.Plugins.Providers
{
    public class PluginContextProvider : BaseContextProvider<PluginMetadataContext>
    {
        public PluginContextProvider(IServiceProvider services, ITypeAdapterFactory typeAdapterFactory)
            : base(new PluginsContext(services), typeAdapterFactory)
        {
        }
    }
}