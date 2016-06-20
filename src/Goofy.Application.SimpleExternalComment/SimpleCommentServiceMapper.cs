using Goofy.Application.Core.Service;
using Goofy.Domain.Core.Entity;
using Goofy.Domain.Core.Service.Adapter;
using Goofy.Domain.Core.Service.Data;
using Goofy.Domain.SimpleExternalComment.Service.Adapter;
using Goofy.Domain.SimpleExternalComment.Service.Data;

namespace Goofy.Application.SimpleExternalComment
{
    public class SimpleCommentServiceMapper<TEntity, TViewModel> : ServiceMapper<TEntity, TViewModel>, ISimpleCommentServiceMapper<TEntity, TViewModel>
        where TEntity : BaseEntity
        where TViewModel : class
    {
        public SimpleCommentServiceMapper(ISimpleCommentUnitOfWork unitOfWork, ITypeAdapter typeAdapter, IRepository<TEntity> repository = null)
            : base(unitOfWork, typeAdapter, repository)
        {
        }
    }
}
