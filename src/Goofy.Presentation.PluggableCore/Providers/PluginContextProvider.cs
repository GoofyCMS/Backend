using Goofy.Domain.Core.Service.Adapter;
using Goofy.Infrastructure.PluggableCore.Data;
using Goofy.Presentation.Core.Providers;
using System;

namespace Goofy.Presentation.PluggableCore.Providers
{
    public class PluginContextProvider : BaseContextProvider<PluginMetadataContext>
    {
        public PluginContextProvider(IServiceProvider services, ITypeAdapterFactory typeAdapterFactory)
            : base(new PluginsContext(services), typeAdapterFactory)
        {
        }
    }
}