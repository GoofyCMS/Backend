using System;
using Microsoft.Extensions.DependencyInjection;
using Goofy.Domain.Core.Service.Adapter;
using Goofy.Presentation.Core.Providers;
using Goofy.Domain.PluggableCore.Service.Data;

namespace Goofy.Presentation.PluggableCore.Providers
{
    public class PluginContextProvider : BaseContextProvider<PluginMetadataContext>
    {
        public PluginContextProvider(IServiceProvider services, ITypeAdapterFactory typeAdapterFactory)
            : base(services.GetRequiredService<IPluginUnitOfWork>(), typeAdapterFactory)
        {
        }
    }
}