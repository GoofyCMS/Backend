using System;
using Goofy.Core.Components.Configuration;

namespace Goofy.Component.ComponentsWebInterface.Configuration
{
    public class ComponentsWebInterfaceConfigConfigurator : IConfigurator<ComponentsWebInterfaceConfiguration>
    {
        public void Configure(ComponentsWebInterfaceConfiguration config)
        {
            config.CompConfig.IsSystemPlugin = true;
        }
    }
}
