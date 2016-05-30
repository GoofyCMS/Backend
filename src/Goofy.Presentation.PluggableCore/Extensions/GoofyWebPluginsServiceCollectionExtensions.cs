using Goofy.Application.Plugins;
using Goofy.Presentation.PluggableCore.Providers;
using Goofy.WebFramework.Mvc;
using Microsoft.AspNet.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;

namespace Goofy.Presentation.PluggableCore.Extensions
{
    public static class GoofyWebPluginsServiceCollectionExtensions
    {
        public static void AddMvcServices(this IServiceCollection services)
        {
            services.AddScoped<PluginContextProvider>();
            services.AddMvc();

            services.AddSingleton<IAssemblyProvider, GoofyMvcAssemblyProvider>(
            provider =>
                {
                    return new GoofyMvcAssemblyProvider(
                        provider.GetRequiredService<ILibraryManager>(),
                        provider.GetRequiredService<IPluginAssemblyProvider>()
                        );
                });
        }
    }
}
