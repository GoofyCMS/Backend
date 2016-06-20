using Goofy.Domain.Core.Entity;
using Goofy.Domain.Core.Service.Adapter;

namespace Goofy.Domain.SimpleExternalComment.Service.Adapter
{
    public interface ISimpleCommentServiceMapper<TEntity, TViewModel> : IServiceMapper<TEntity, TViewModel>
        where TEntity : BaseEntity
        where TViewModel : class
    {
    }
}
