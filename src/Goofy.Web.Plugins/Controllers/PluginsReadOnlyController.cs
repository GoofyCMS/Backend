using Breeze.ContextProvider;
using Goofy.Domain.Core.Service.Adapter;
using Goofy.Domain.Plugins.Entity;
using Goofy.Application.Plugins.DTO;
using Goofy.Web.Core.Controllers;

namespace Goofy.Web.Plugins.Controllers
{
    public class PluginsReadOnlyController : BaseReadOnlyController<Plugin, PluginItem, int>
    {
        public PluginsReadOnlyController(IServiceMapper<Plugin, PluginItem> service, ContextProvider provider)
            : base(service, provider)
        {
        }
    }
}
