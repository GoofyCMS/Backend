using System;
using Microsoft.Extensions.Configuration;

using Goofy.Core.Components.Configuration;
using Goofy.Extensions;
using Goofy.Core.Components.Base;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class GoofyConfigurationsServiceCollectionExtensions
    {
        public static void Configure<GConfig, GConfigurator>(this IServiceCollection services) where GConfig : ComponentConfig, new() 
                                                                                               where GConfigurator : IConfigurator<GConfig>, new()
        {
            var configurator = new GConfigurator();
            services.Configure<GConfig>(b => configurator.Configure(b));
        }

        public static void ConfigureComponentConfigurationFile<GConfig>(this IServiceCollection services, string componentName, string configFileName = "config.json", string configSection = null) where GConfig : ComponentConfig
        {
            if (componentName == null)
            {
                throw new ArgumentNullException();
            }
            if (configSection == null)
            {
                configSection = componentName;
            }
            var componentPathProvider = services.Resolve<IComponentsDirectoryPathProvider>();
            var configFilePath = string.Format("{0}\\{1}\\{2}", componentPathProvider.GetComponentsDirectoryPath(), componentName, configFileName);
            var configurationBuilder = services.Resolve<ConfigurationBuilder>();
            configurationBuilder.AddJsonFile(configFilePath);
            var configuration = configurationBuilder.Build();
            services.Configure<GConfig>(configuration.GetSection(configSection));
        }
    }
}
