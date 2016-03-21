using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Goofy.WebFramework.Core.Extensions
{
    public static class ComponentManagerExtensions
    {
        public static IConfiguration AddComponentsConfigurationFiles(this ConfigurationBuilder configurationBuilder, IEnumerable<string> componentsConfigurationFilesPath)
        {
            foreach (var jsonConfigFilePath in componentsConfigurationFilesPath)
                configurationBuilder.AddJsonFile(jsonConfigFilePath);

            return configurationBuilder.Build();
        }
    }
}
