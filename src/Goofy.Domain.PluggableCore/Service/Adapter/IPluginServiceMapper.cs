using Goofy.Domain.Core.Entity;
using Goofy.Domain.Core.Service.Adapter;

namespace Goofy.Domain.PluggableCore.Service.Adapter
{
    public interface IPluginServiceMapper<TEntity, TViewModel> : IServiceMapper<TEntity, TViewModel>
        where TEntity : BaseEntity
        where TViewModel : class
    {
    }
}
