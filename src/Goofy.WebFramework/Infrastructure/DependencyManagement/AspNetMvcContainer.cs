using System;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

using Goofy.Core.Infrastructure;
using Goofy.Data;
using Goofy.Core.Components.Configuration;

namespace Goofy.WebFramework.Infrastructure
{
    public class AspNetMvcContainer : IWebDependencyContainer
    {
        private int _lastNumberServices;

        public AspNetMvcContainer(IServiceCollection serviceCollection, EntityFrameworkServicesBuilder builder)
        {
            ServiceCollection = serviceCollection;
            ServiceProvider = serviceCollection.BuildServiceProvider();
            _lastNumberServices = serviceCollection.Count();
            EntityFrameworkServicesBuilder = builder;
        }

        private EntityFrameworkServicesBuilder EntityFrameworkServicesBuilder { get; set; }

        public IServiceCollection ServiceCollection { get; private set; }

        public IServiceProvider ServiceProvider { get; private set; }

        public void AddDbContextObject<T>() where T : DbContext
        {
            /*
                Esto lo tuve que hacer porque EntityFrameworkServicesBuilder no me
                deja agregar DbContext objects teniendo su type, el método que lo hace
                sólo deja hacerlo de forma genérica como en la siguiente línea
            */
            EntityFrameworkServicesBuilder.AddDbContext<T>(b => { GoofyDataAccessManager.GoofyDataConfiguration.Provider.ConfigureDbContextProvider(b); });
        }

        public bool IsRegistered(Type serviceType)
        {
            return ServiceCollection
                .Select(a => a.ServiceType)
                .Where(a => a == serviceType)
                .Count() != 0;
        }

        public void Register<T>() where T : class
        {
            ServiceCollection.AddScoped<T>();
        }

        public void RegisterConfigurations<TSetup>(IConfiguration configuration) where TSetup : ComponentConfig
        {
            ServiceCollection.Configure<TSetup>(configuration);
        }

        public void RegisterDependency<TService, TDependency>() where TService : TDependency
        {
            // AddScoped -> Significa que se crea una única instancia por Request
            ServiceCollection.AddScoped(typeof(TDependency), typeof(TService));
        }

        public void RegisterDependency(Type serviceType, Type dependencyType)
        {
            ServiceCollection.AddScoped(dependencyType, serviceType);
        }


        public void RegisterDependencyAsSingleton<TService, TDependency>() where TService : TDependency
        {
            // AddSingleton -> Significa que se crea una única instancia para toda la ejecución
            ServiceCollection.AddSingleton(typeof(TDependency), typeof(TService));
        }

        public void RegisterInstanceAsSingleton<T>(T instance) where T : class
        {
            // AddInstance -> Significa que la instancia agregada se mantiene para toda la ejecución
            ServiceCollection.AddInstance(instance);
        }

        public object Resolve(Type serviceType)
        {
            UpdateServiceProviderIsNeeded();
            return ActivatorUtilities.CreateInstance(ServiceProvider, serviceType);
        }

        public object Resolve(Type serviceType, IScope scope)
        {
            return Resolve(serviceType);
        }

        public T Resolve<T>()
        {
            UpdateServiceProviderIsNeeded();
            return ActivatorUtilities.CreateInstance<T>(ServiceProvider);
        }

        private void UpdateServiceProviderIsNeeded()
        {
            if (_lastNumberServices != ServiceCollection.Count)
            {
                ServiceProvider = ServiceCollection.BuildServiceProvider();
            }
        }

        public IScope Scope()
        {
            throw new NotImplementedException();
        }
    }
}
