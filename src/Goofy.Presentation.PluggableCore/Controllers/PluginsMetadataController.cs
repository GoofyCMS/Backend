using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Mvc;
using Goofy.Web.Core.Controllers;
using Goofy.Presentation.PluggableCore.Providers;

namespace Goofy.Presentation.PluggableCore.Controllers
{
    [Route("api/plugins")]
    public class PluginsMetadataController : BaseMetadataController
    {
        public PluginsMetadataController(IServiceProvider serviceProvider)
            : base(serviceProvider.GetRequiredService<PluginContextProvider>())
        {
        }
    }
}
