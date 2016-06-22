using Goofy.Domain.Core.Entity;
using Goofy.Domain.Core.Service.Adapter;

namespace Goofy.Domain.Identity.Services.Adapter
{
    public interface IAdministrationServiceMapper<TEntity, TViewModel> : IServiceMapper<TEntity, TViewModel>
        where TEntity : BaseEntity
        where TViewModel : class
    {
    }
}
