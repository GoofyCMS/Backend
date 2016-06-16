using Goofy.Application.Administration.Services.Abstractions;
using Goofy.Application.Core.Abstractions;
using Goofy.Domain.Administration.Entity;
using Goofy.Domain.Administration.Service.Data;
using Goofy.Domain.Core;
using Goofy.Domain.Core.Service.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Goofy.Application.Administration
{
    public class SynchronizePluginTables : IRunAtStartup
    {
        private readonly IServiceProvider _services;
        private readonly IPluginManager _pluginManager;
        private readonly IAdministrationUnitOfWork _pluginsContext;

        public SynchronizePluginTables(IServiceProvider services, IPluginManager pluginManager, IAdministrationUnitOfWork pluginsContext)
        {
            _services = services;
            _pluginManager = pluginManager;
            _pluginsContext = pluginsContext;
        }

        public int Order
        {
            get
            {
                return -900;
            }
        }

        public void Run()
        {
            var pluginsRepository = _pluginsContext.Set<Plugin>();
            foreach (var plugin in pluginsRepository.GetAll())
            {
                var unitOfWorkType = _pluginManager.GetAssembliesByPluginName(plugin.Name).FindClassesOfType(typeof(IUnitOfWork)).FirstOrDefault();
                if (unitOfWorkType != null)
                {
                    var unitOfWork = (IUnitOfWork)_services.GetRequiredService(unitOfWorkType);
                }
            }
        }
    }
}
