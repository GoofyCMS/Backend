using Microsoft.Extensions.DependencyInjection;
using Goofy.Core.Infrastructure;
using Goofy.Component.ComponentsWebInterface.Configuration;

namespace Goofy.Component.ComponentsWebInterface.Services
{
    public class ComponentsWebInterfaceDependencyAssembler : IDependencyAssembler
    {
        public int Order
        {
            get
            {
                return 0;
            }
        }

        public void Register(IServiceCollection services)
        {
            services.Configure<ComponentsWebInterfaceConfiguration, ComponentsWebInterfaceConfigConfigurator>();
        }
    }
}
