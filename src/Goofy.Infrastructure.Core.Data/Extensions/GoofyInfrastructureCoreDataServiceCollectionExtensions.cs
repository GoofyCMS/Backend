using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.Entity.Internal;
using Microsoft.Data.Entity.Infrastructure;
using Goofy.Infrastructure.Core.Data.Context;
using Goofy.Infrastructure.Core.Data.Service;

namespace Goofy.Infrastructure.Core.Data
{
    public static class GoofyInfrastructureCoreDataServiceCollectionExtensions
    {
        public static IServiceCollection AddUnitOfWork<T>(this IServiceCollection services) where T : UnitOfWork
        {
            var dbContextRegistrarType = typeof(DbContextRegistrar<>).MakeGenericType(new[] { typeof(T) });
            dynamic dbContextRegistrar = Activator.CreateInstance(dbContextRegistrarType);
            dbContextRegistrar.AddDbContext(new EntityFrameworkServicesBuilder(services));


            services.AddScoped(typeof(T), serviceProvider => { return DbContextActivator.CreateInstance<T>(serviceProvider); });
            return services;
        }
    }
}
