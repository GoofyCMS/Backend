using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Authorization;
using Goofy.Security.Services;
using System.Linq;

namespace Goofy.Security.Extensions
{
    public static class SecurityServiceCollectionExtensions
    {
        public static Dictionary<string, IEnumerable<CrudOperation>> Resources { get; set; } = new Dictionary<string, IEnumerable<CrudOperation>>();

        public static void AddEntireCrudPermissions(this IServiceCollection services, params Type[] resources)
        {
            var resourcesName = resources.Select(t => ResourceNameFromType(t)).ToArray();
            services.AddEntireCrudPermissions(resourcesName);
        }

        private static string ResourceNameFromType(Type t)
        {
            return t.FullName.Substring(t.FullName.LastIndexOf('.') + 1);
        }

        private static void AddEntireCrudPermissions(this IServiceCollection services, params string[] resourceName)
        {
            if (Resources == null)
            {
                throw new InvalidOperationException();
            }

            foreach (var r in resourceName)
            {
                services.AddCrudPermissions(r, new[] { CrudOperation.Create, CrudOperation.Read, CrudOperation.Update, CrudOperation.Delete });
            }
        }

        private static void AddCrudPermissions(this IServiceCollection services, string resource, params CrudOperation[] crudPermissions)
        {
            if (Resources == null)
            {
                throw new InvalidOperationException();
            }

            if (Resources.ContainsKey(resource))
            {
                throw new Exception($"Permission \"{resource}\" already exists");
            }
            Resources.Add(resource, crudPermissions);
        }

        public static IServiceCollection BuildPermissions(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                foreach (var res in Resources)
                {
                    options.AddResourceCrudPermissions(res);
                }
                Resources = null; /*La forma en la que esto se procesa me permite seguir agregando 
                                        recursos hasta que no se haga el primer request*/
            });
            services.AddSingleton<IAuthorizationHandler, CustomRequireClaimHandler>();

            return services;
        }
    }
}