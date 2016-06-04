using Goofy.Application.Core.Service;
using Goofy.Domain.Core.Entity;
using Goofy.Domain.Core.Service.Adapter;
using Goofy.Domain.Core.Service.Data;
using Goofy.Infrastructure.Blog.Data;

namespace Goofy.Application.Blog
{
    public class BlogServiceMapper<TEntity, TViewModel> : ServiceMapper<TEntity, TViewModel>
        where TEntity : BaseEntity
        where TViewModel : class
    {
        //public BlogServiceMapper(IBlogUnitOfWork unitOfWork, ITypeAdapter typeAdapter, IRepository<TEntity> repository = null)
        public BlogServiceMapper(BlogContext unitOfWork, ITypeAdapter typeAdapter, IRepository<TEntity> repository = null)
            : base(unitOfWork, typeAdapter, repository)
        {
        }
    }
}
