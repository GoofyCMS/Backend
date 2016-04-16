using Microsoft.Extensions.DependencyInjection;

using Goofy.Core.Infrastructure;
using Goofy.Component.Administration.Configuration;

namespace Goofy.Component.Administration.Services
{
    public class AdministrationDependencyAssembler : IDependencyAssembler
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
            services.ConfigureComponentConfigurationFile<AdministrationConfiguration>("Goofy.Component.Administration");
        }
    }
}
