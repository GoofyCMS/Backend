using Goofy.Domain.Core.Entity;
using Goofy.Domain.Core.Service.Adapter;

namespace Goofy.Domain.Blog.Service.Adapter
{
    public interface IBlogServiceMapper<TEntity, TViewModel> : IServiceMapper<TEntity, TViewModel>
        where TEntity : BaseEntity
        where TViewModel : class
    {
    }
}
