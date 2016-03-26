using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.OptionsModel;

namespace Goofy.Extensions
{
    public static class ConfigurationExtensions
    {
        public static T GetConfiguration<T>(this IServiceCollection services) where T : class, new()
        {
            var configOptions = services.Resolve<IOptions<T>>();
            return configOptions.Value;
        }
    }
}
