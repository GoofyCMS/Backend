using Microsoft.Extensions.Configuration;
using Goofy.Core.Infrastructure;
using Goofy.WebFramework.Infrastructure;
using Goofy.Data;


namespace Goofy.Component.ControllersAndRoutes
{
    public class ControllersAndRoutesDependencyAssembler : IGoofyDependencyAssembler
    {
        public int Order
        {
            get
            {
                return 1;
            }
        }

        public void Register(IDependencyContainer container, IResourcesLoader loader)
        {
            container.RegisterDependency<FileSystemWriter, IWriter>();   
        }

        public void RegisterWebDependencies(IWebDependencyContainer container, IResourcesLoader loader, IConfiguration config)
        {
            container.RegisterConfigurations<ControllerAndRoutesConfiguration>(config.GetSection("Goofy.Component.ControllersAndRoutes"));
            container.AddDbContextObject<BookContext>();
        }
    }
}
