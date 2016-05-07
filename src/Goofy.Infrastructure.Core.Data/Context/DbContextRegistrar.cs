using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

namespace Goofy.Infrastructure.Core.Data.Context
{
    public class DbContextRegistrar<T> where T : DbContext
    {
        /*
            Esta clase probablemente no la voy a usar porque los objetos que se
            van a agregar son IRepository, IUnitOfWork que por dentro deberían hacer exactamente
            esto.
        */
        public void AddDbContext(EntityFrameworkServicesBuilder builder)
        {
            builder.AddDbContext<T>();
        }
    }
}
