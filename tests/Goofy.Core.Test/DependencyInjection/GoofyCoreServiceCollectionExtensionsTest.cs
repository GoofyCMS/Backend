using Microsoft.Extensions.DependencyInjection;
using Goofy.Core.Infrastructure;

using System.Linq;
using Xunit;
using Goofy.Core.Components.Base;
using Microsoft.Extensions.OptionsModel;
using Goofy.Core.Configuration;
using Microsoft.Extensions.Configuration;

namespace Goofy.Core.Test.DependencyInjection
{
    public class GoofyCoreServiceCollectionExtensionsTest
    {
        [Fact]
        public virtual void Services_wire_up_correctly()
        {
            VerifyCanResolve<IOptions<GoofyCoreConfiguration>>();
            VerifySingleton<ConfigurationBuilder>();
            VerifyScoped<IResourcesLocator>();
            VerifyScoped<IAssembliesProvider>();
            VerifyScoped<IComponentsDirectoryPathProvider>();
            VerifySingleton<IComponentsAssembliesProvider>();
            VerifyScoped<IComponentsInfoProvider>();
            VerifySingleton<IEngine>();
        }


        IServiceCollection _goofyCoreServices;
        public GoofyCoreServiceCollectionExtensionsTest()
        {
            _goofyCoreServices = AddServices(new ServiceCollection());
        }
        private IServiceCollection AddServices(IServiceCollection serviceCollection)
        {
            return serviceCollection.AddGoofyCore();
        }

        private void VerifyCanResolve<TService>()
        {
            var serviceProvider = _goofyCoreServices.BuildServiceProvider();
            serviceProvider.GetService<TService>();
        }
        private void VerifySingleton<TService>()
        {
            Verify<TService>(ServiceLifetime.Singleton);
        }
        private void VerifyTransient<TService>()
        {
            Verify<TService>(ServiceLifetime.Transient);
        }
        private void VerifyScoped<TService>()
        {
            Verify<TService>(ServiceLifetime.Scoped);
        }
        private void Verify<TService>(ServiceLifetime? serviceLifetime = null)
        {
            var serviceDescriptor = _goofyCoreServices.Where(s => s.ServiceType == typeof(TService)).FirstOrDefault();
            Assert.NotNull(serviceDescriptor);
            if (serviceLifetime != null)
                Assert.Equal(serviceLifetime, serviceDescriptor.Lifetime);
        }
    }
}
