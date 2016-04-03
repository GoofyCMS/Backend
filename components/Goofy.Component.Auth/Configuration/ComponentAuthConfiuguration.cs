using Goofy.Core.Components.Configuration;

namespace Goofy.Component.Auth.Configuration
{
    public class ComponentAuthConfiuguration : ComponentConfig
    {
        public ComponentAuthConfiuguration()
        {
            CompConfig.IsSystemPlugin = true;
        }
    }
}
