using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

using Goofy.Extensions;

namespace Goofy.WebFramework.Mvc.DependencyInjection
{
    public static class GoofyWebMvcServiceCollectionExtensions
    {
        public static IServiceCollection AddWebGoofyMvc(this IServiceCollection services)
        {
            services.Remove<IActionSelector>(true);
            services.AddScoped<IActionSelector, GoofyActionSelector>();
            return services;
        }
    }
}
