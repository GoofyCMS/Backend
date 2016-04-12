using System.Collections.Generic;
using Goofy.Core.Components.Configuration;

namespace Goofy.Component.CorsIntegration.Configuration
{
    public class CorsConfiguration : ComponentConfig
    {
        public CorsPolicyConfiguration[] Policies { get; set; } = new CorsPolicyConfiguration[] { };
    }
}
