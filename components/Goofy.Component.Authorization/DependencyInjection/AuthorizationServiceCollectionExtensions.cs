using System;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;
using Goofy.Component.Authorization.Model;
using Goofy.Component.Authorization.Resources;
using Goofy.Extensions;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Goofy.Component.Authorization.DependencyInjection
{
    public static class AuthorizationServiceCollectionExtensions
    {
        private static IServiceCollection AddGoofyCrudPolicies(this IServiceCollection services, IEnumerable<GoofyCrudPolicy> crudPolicies)
        {
            services.AddAuthorization(options =>
            {
                foreach (var crudPolicy in crudPolicies)
                {
                    options.AddPolicy(crudPolicy.PolicyName, crudPolicy.PolicyBuilder);
                }
            });
            return services;
        }

        public static IServiceCollection AddGoofyCrudPolicies(this IServiceCollection services, IEnumerable<Type> entityTypes)
        {
            var policiesManager = services.Resolve<IGoofyCrudPoliciesManager>();
            var policies = entityTypes.SelectMany(t =>
            {
                return new[] {
                                policiesManager.AddNewGoofyCrudPolicy(t, CrudOperation.Create),
                                policiesManager.AddNewGoofyCrudPolicy(t, CrudOperation.Update),
                                policiesManager.AddNewGoofyCrudPolicy(t, CrudOperation.Delete)
                             };

            });
            services.AddGoofyCrudPolicies(policies);
            var logger = services.Resolve<ILogger<IGoofyCrudPoliciesManager>>();
            foreach (var policy in policies)
            {
                logger.LogInformation(string.Format("New policy \"{0}\" added to Asp.Net Authorization system", policy.PolicyName));
            }
            return services;
        }
    }
}
