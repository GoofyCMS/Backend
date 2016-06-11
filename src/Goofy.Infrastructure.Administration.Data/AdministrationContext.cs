using Goofy.Domain.Administration.Entity;
using Goofy.Domain.Administration.Service.Data;
using Goofy.Infrastructure.Identity.Data.Service;

namespace Goofy.Infrastructure.Administration.Data
{
    public class AdministrationContext : IdentityUnitOfWork<GoofyUser>, IAdministrationUnitOfWork
    {
        public AdministrationContext(string connectionString)
            : base(connectionString)
        {
        }
    }
}
