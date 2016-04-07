using Goofy.Core.Components.Configuration;

namespace Goofy.Component.Auth.Configuration
{
    public class ComponentAuthConfiguration : ComponentConfig
    {
        public ComponentAuthConfiguration()
        {
            CompConfig.IsSystemPlugin = true;
        }
    }
}
