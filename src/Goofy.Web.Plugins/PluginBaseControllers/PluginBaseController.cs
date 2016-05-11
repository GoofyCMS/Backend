using Breeze.ContextProvider;
using Goofy.Domain.Core.Entity;
using Goofy.Domain.Core.Service.Adapter;
using Goofy.Web.Core.Controllers;

namespace Goofy.Web.Plugins.PluginBaseControllers
{
    public class PluginBaseController<TEntity, TViewModel, TKey> : BaseController<TEntity, TViewModel, TKey>
        where TEntity : BaseEntity where TViewModel : class
    {
        public PluginBaseController(IServiceMapper<TEntity, TViewModel> serviceMapper, ContextProvider provider)
            : base(serviceMapper, provider)
        {
        }
    }
}
