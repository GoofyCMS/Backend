﻿using Breeze.ContextProvider;
using Goofy.Domain.Core.Service.Adapter;
using Goofy.Domain.Plugins.Entity;
using Goofy.Application.Plugins.DTO;
using Goofy.Web.Core.Controllers;
using Microsoft.AspNet.Mvc;
using Goofy.Application.Plugins.Services;
using Goofy.Web.Plugins.Providers;

namespace Goofy.Web.Plugins.Controllers
{
    [Route("plugins")]
    public class PluginsReadOnlyController : BaseReadOnlyController<Plugin, PluginItem, int>
    {
        public PluginsReadOnlyController(PluginServiceMapper<Plugin, PluginItem> service, PluginContextProvider provider)
            : base(service, provider)
        {
        }

        /* con objetivos de testing*/
        [Route("test")]
        public string PluginReadOnlyController()
        {
            return "Hello from PluginsReadOnlyController";
        }
    }
}
