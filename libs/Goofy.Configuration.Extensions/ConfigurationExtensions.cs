using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.OptionsModel;

namespace Goofy.Configuration.Extensions
{
    public static class ConfigurationExtensions
    {
        public static T GetConfiguration<T>(this IConfiguration configuration) where T : class, new()
        {
            T configurationInstance = new T();
            var confOptions = new ConfigureFromConfigurationOptions<T>(configuration);
            confOptions.Configure(configurationInstance);
            return configurationInstance;
        }

        public static T GetConfiguration<T>(string jsonDataSource, string dataSection) where T : class, new()
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(jsonDataSource);
            var config = builder.Build();
            return config.GetSection(dataSection).GetConfiguration<T>();
        }

    }
}
