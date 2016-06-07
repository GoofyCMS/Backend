using System;
using Goofy.Application.PluggableCore.Abstractions;
using Goofy.Domain.PluggableCore.Service.Data;
using Goofy.Domain.PluggableCore.Entity;
using Goofy.Domain.Core.Service.Data;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Goofy.Infrastructure.Core.Data.Utils;
using Goofy.Domain.Core;

namespace Goofy.Application.PluggableCore
{
    public class SynchronizePluginTables : IRunAtStartup
    {
        private readonly IServiceProvider _services;
        private readonly IPluginManager _pluginManager;
        private readonly IPluginUnitOfWork _pluginsContext;

        public SynchronizePluginTables(IServiceProvider services, IPluginManager pluginManager, IPluginUnitOfWork pluginsContext)
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
                    if (plugin.Enabled)
                    {
                        //create tables if not exist
                        unitOfWork.CreateTablesIfNotExist();
                    }
                    else
                    {
                        //delete tables if exist
                        unitOfWork.DropTables();
                    }
                }
            }
        }
    }
}
