﻿using Microsoft.Extensions.DependencyInjection;
using Goofy.Core.Infrastructure;
using Goofy.Component.CorsIntegration.Configuration;
using Goofy.Component.CorsIntegration.CorsExtensions;
using Goofy.Extensions;

namespace Goofy.Component.CorsIntegration.Services
{
    public class CorsDependencyAssembler : IDependencyAssembler
    {
        public int Order
        {
            get
            {
                return 0;
            }
        }

        public void Register(IServiceCollection services)
        {
            services.ConfigureComponentConfigurationFile<CorsConfiguration>("Goofy.Component.CorsIntegration");
            var corsConfig = services.GetConfiguration<CorsConfiguration>();
            services.AddPolicies(corsConfig);
        }
    }
}
