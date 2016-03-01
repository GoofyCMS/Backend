using Microsoft.Extensions.DependencyInjection;

using Goofy.WebFramework.Infrastructure;
using Goofy.Component.CorsIntegration.Configuration;

namespace Goofy.Component.CorsIntegration.CorsExtensions
{
    public static class DependencyInjection
    {
        public static void AddPolicies(this IWebDependencyContainer dependencyContainer, CorsConfiguration corsConfig)
        {
            dependencyContainer.ServiceCollection.AddCors(
                options =>
                {
                    foreach (var key in corsConfig.Policies.Keys)
                    {
                        options.AddPolicy(key,
                            builder =>
                            {
                                builder.WithOrigins(corsConfig.Policies[key]).AllowAnyMethod().AllowAnyHeader();
                            });
                    }
                }
            );
        }


    }
}
