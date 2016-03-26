using Goofy.Core.Components.Configuration;
using System.Collections.Generic;

namespace Goofy.Component.CorsIntegration.Configuration
{
    public class CorsConfigConfigurator : IConfigurator<CorsConfiguration>
    {
        public void Configure(CorsConfiguration configInstance)
        {
            configInstance.CompConfig.IsSystemPlugin = true;
            configInstance.Policies = new Dictionary<string, string[]>()
                                      {
                                        { "AllowYoel", new [] { "http://192.168.249.1:3000" } },
                                        { "Policy2", new [] { "http://192.168.249.151" } }
                                      };
        }
    }
}
