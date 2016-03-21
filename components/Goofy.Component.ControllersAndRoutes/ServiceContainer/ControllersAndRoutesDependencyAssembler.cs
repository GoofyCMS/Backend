using Microsoft.Extensions.DependencyInjection;

using Goofy.Core.Infrastructure;

using Goofy.WebFramework.Data.DependencyInjection;

namespace Goofy.Component.ControllersAndRoutes
{
    public class ControllersAndRoutesDependencyAssembler : IDependencyAssembler
    {
        public int Order
        {
            get
            {
                return 1;
            }
        }

        public void Register(IServiceCollection services, IResourcesLoader loader)
        {
            services.AddScoped<IWriter, FileSystemWriter>();
            services.AddDbContextObject<BookContext>();
        }
    }
}
