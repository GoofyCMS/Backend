using Goofy.Application.Administration.Services.Abstractions;
using Goofy.Application.Core.Abstractions;
using Goofy.Presentation.Services;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Cors;
using Microsoft.AspNet.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;

namespace Goofy.Presentation.DependencyInjection
{
    public static class GoofyPresentationServiceCollectionExtensions
    {
        public static void AddMvcServices(this IServiceCollection services)
        {
            services.AddMvc();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowNoOne", builder => builder.WithOrigins("http://192.168.1.2:8000", "http://localhost:8000")
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
                    provider.GetRequiredService<IPluginManager>(),
                    provider.GetRequiredService<IGoofyAssemblyProvider>()
                    );
            });
            services.AddScoped<IActionSelector, GoofyActionSelector>();
        }
    }
}
