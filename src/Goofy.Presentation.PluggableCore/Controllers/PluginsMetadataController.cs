﻿using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Mvc;
using Goofy.Presentation.Core.Controllers;
using Goofy.Presentation.PluggableCore.Providers;

namespace Goofy.Presentation.PluggableCore.Controllers
{
    [Route("api/plugin")]
    public class PluginsMetadataController : BaseMetadataController
    {
        public PluginsMetadataController(IServiceProvider serviceProvider)
            : base(serviceProvider.GetRequiredService<PluginContextProvider>())
        {
        }
    }
}
