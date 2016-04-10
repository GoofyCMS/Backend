using Goofy.Core.Components.Configuration;

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
    }
}
