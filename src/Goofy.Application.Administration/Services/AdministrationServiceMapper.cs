using Goofy.Application.Core.Service;
using Goofy.Domain.Administration.Service.Data;
using Goofy.Domain.Core.Entity;
using Goofy.Domain.Core.Service.Adapter;
using Goofy.Domain.Core.Service.Data;
using Goofy.Domain.Identity.Services.Adapter;

namespace Goofy.Application.Administration.Services
{
    public class AdministrationServiceMapper<TEntity, TViewModel> : ServiceMapper<TEntity, TViewModel>, IAdministrationServiceMapper<TEntity, TViewModel>
        where TEntity : BaseEntity
        where TViewModel : class
    {
        public AdministrationServiceMapper(IAdministrationUnitOfWork unitOfWork, ITypeAdapter typeAdapter, IRepository<TEntity> repository = null)
            : base(unitOfWork, typeAdapter, repository)
        {
        }
    }
}
