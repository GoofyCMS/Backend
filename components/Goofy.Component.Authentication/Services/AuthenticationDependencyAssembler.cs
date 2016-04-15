using System;
using Goofy.Core.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Goofy.Component.Authentication.Configuration;

namespace Goofy.Component.Authentication.Services
{
    public class AuthenticationDependencyAssembler : IDependencyAssembler
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
            services.ConfigureComponentConfigurationFile<AuthenticationConfiguration>("Goofy.Component.Authentication");
        }
    }
}
