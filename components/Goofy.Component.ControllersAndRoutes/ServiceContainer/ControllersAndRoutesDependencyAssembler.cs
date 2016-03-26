using Microsoft.Extensions.DependencyInjection;

using Goofy.Core.Infrastructure;

namespace Goofy.Component.ControllersAndRoutes
{
    public class ControllersAndRoutesDependencyAssembler : IDependencyAssembler
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
            services.Configure<ControllerAndRoutesConfiguration>(a => { });
            services.AddScoped<IWriter, FileSystemWriter>();
            services.AddDbContextObject<BookContext>();
        }
    }
}
