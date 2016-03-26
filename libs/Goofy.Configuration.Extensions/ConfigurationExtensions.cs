using Microsoft.Extensions.OptionsModel;

namespace Microsoft.Extensions.DependencyInjection
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
