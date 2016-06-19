using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.OptionsModel;

namespace Goofy.Presentation.Configuration.Extensions
{
    public static class DesignTimeConfigurationExtensions
    {
        public static T GetConfiguration<T>(this IConfiguration configuration) where T : class, new()
        {
            var configurationInstance = new T();
            var confOptions = new ConfigureFromConfigurationOptions<T>(configuration);
            confOptions.Configure(configurationInstance);
            return configurationInstance;
        }
    }

}
