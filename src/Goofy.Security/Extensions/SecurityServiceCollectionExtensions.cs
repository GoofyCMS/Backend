using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Goofy.Security.Extensions
{
    public static class SecurityServiceCollectionExtensions
    {
        static HashSet<string> Resources { get; set; } = new HashSet<string>();

        public static void AddCrudPermissions(this IServiceCollection services, params string[] resources)
        {
            if (Resources == null)
            {
                throw new InvalidOperationException();
            }
            foreach (var r in resources)
            {
                if (Resources.Contains(r))
                {
                    throw new Exception($"Permission \"{r}\" already exist");
                }
                Resources.Add(r);
            }
        }

        public static IServiceCollection BuildPermissions(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                foreach (var res in Resources)
                {
                    options.AddResourceCrudPermissions(res);
                }
            });

            Resources = null;
            return services;
        }
    }
}
