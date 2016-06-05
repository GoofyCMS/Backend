using Goofy.Application.PluggableCore.Abstractions;
using Goofy.Presentation.PluggableCore.Providers;
using Goofy.Presentation.PluggableCore.Services;
using Goofy.WebFramework.Mvc;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Cors;
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
            services.AddScoped(typeof(PluginDependenciesAdder), typeof(PluginsContextAdder));
            services.AddMvc();
            services.AddCors(options => 
                             {
                                 options.AddPolicy("AllowNoOne", builder => builder.WithOrigins("http://192.168.1.2:8000")
                                                                                 .AllowAnyHeader()
                                                                                 .AllowAnyMethod()
                                                                                 .DisallowCredentials());
                             });
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowNoOne"));
            });

            services.AddSingleton<IAssemblyProvider, GoofyMvcAssemblyProvider>(
            provider =>
                {
                    return new GoofyMvcAssemblyProvider(
                        provider.GetRequiredService<ILibraryManager>(),
                        provider.GetRequiredService<IPluginManager>()
                        );
                });
        }
    }
}
