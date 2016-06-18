using Goofy.Application.Administration.Services.Abstractions;
using Goofy.Application.Core.Abstractions;
using Goofy.Presentation.Configuration;
using Goofy.Presentation.Services;
using Microsoft.AspNet.Cors.Infrastructure;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Cors;
using Microsoft.AspNet.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;

namespace Goofy.Presentation.DependencyInjection
{
    public static class GoofyPresentationServiceCollectionExtensions
    {
        public static void AddCorsPolicies(this IServiceCollection services, CorsConfiguration corsConfig)
        {
            services.AddCors(
                options =>
                {
                    foreach (var policy in corsConfig.Policies)
                    {
                        options.AddPolicy(policy.Name, builder => { AddPolicy(builder, policy); });
                    }
                }
            );
        }

        private static void AddPolicy(CorsPolicyBuilder policyBuilder, CorsPolicyConfiguration policy)
        {
            if (policy.Origins.Length == 0)
            {
                throw new System.ArgumentException();
            }
            policyBuilder.WithOrigins(policy.Origins);

            // Configure methods
            if (policy.Methods.Length == 0)
                policyBuilder.AllowAnyMethod();
            else
                policyBuilder.WithMethods(policy.Methods);

            // Configure headers
            if (policy.Headers.Length == 0)
                policyBuilder.AllowAnyHeader();
            else
                policyBuilder.WithHeaders(policy.Headers);

            //Configure credentials
            if (policy.AllowCredentials)
                policyBuilder.AllowCredentials();
            else
                policyBuilder.DisallowCredentials();
        }

        public static void AddMvcServices(this IServiceCollection services)
        {
            services.AddMvc();

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowNoOne", builder => builder.WithOrigins("http://localhost:8000")
            //                                                    .AllowAnyHeader()
            //                                                    .AllowAnyMethod()
            //                                                    .DisallowCredentials());
            //});
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
