using Microsoft.AspNet.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

using Goofy.Component.CorsIntegration.Configuration;

namespace Goofy.Component.CorsIntegration.CorsExtensions
{
    public static class DependencyInjection
    {
        public static void AddPolicies(this IServiceCollection services, CorsConfiguration corsConfig)
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

    }
}
