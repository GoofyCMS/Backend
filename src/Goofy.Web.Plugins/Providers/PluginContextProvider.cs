using Goofy.Domain.Core.Service.Adapter;
using Goofy.Infrastructure.Core.Data.Configuration;
using Goofy.Infrastructure.Plugins.Data;
using Goofy.Web.Core.Providers;
using Microsoft.Extensions.OptionsModel;

namespace Goofy.Web.Plugins.Providers
{
    public class PluginContextProvider : BaseContextProvider<PluginMetadataContext>
    {
        public PluginContextProvider(IOptions<DataAccessConfiguration> configurationOptions, ITypeAdapterFactory typeAdapterFactory)
            : base(new PluginsContext(configurationOptions), typeAdapterFactory)
        {
        }
    }
}
