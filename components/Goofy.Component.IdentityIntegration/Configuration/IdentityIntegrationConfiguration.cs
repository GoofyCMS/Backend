using Goofy.Core.Components.Configuration;

namespace Goofy.Component.IdentityIntegration.Configuration
{
    public class IdentityIntegrationConfiguration : ComponentConfig
    {
        public IdentityIntegrationConfiguration()
        {
            CompConfig.IsSystemPlugin = true;
        }
    }
}
