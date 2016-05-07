using Goofy.Infrastructure.Core.Data.Service;
using Microsoft.Extensions.DependencyInjection;


namespace Goofy.Infrastructure.Core.Data.Context
{
    public class IUnitOfWorkRegistrar<T> where T : UnitOfWork
    {
        public void AddUnitOfWork(IServiceCollection services)
        {
            var a = services.AddUnitOfWork<T>();
        }
    }
}
