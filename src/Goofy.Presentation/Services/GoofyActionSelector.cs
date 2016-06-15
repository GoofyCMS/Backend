using Goofy.Application.Administration.Extensions;
using Goofy.Application.Administration.Services.Abstractions;
using Goofy.Application.Core.Extensions;
using Microsoft.AspNet.Mvc.Abstractions;
using Microsoft.AspNet.Mvc.ActionConstraints;
using Microsoft.AspNet.Mvc.Controllers;
using Microsoft.AspNet.Mvc.Infrastructure;
using Microsoft.AspNet.Mvc.Routing;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Goofy.Presentation.Services
{
    public class GoofyActionSelector : DefaultActionSelector
    {
        private readonly IPluginManager _pluginManager;

        public GoofyActionSelector(
                                   IActionDescriptorsCollectionProvider actionDescriptorsCollectionProvider,
                                   IActionSelectorDecisionTreeProvider decisionTreeProvider,
                                   IEnumerable<IActionConstraintProvider> actionConstraintProviders,
                                   ILoggerFactory loggerFactory,
                                   IPluginManager pluginManager)
            : base(actionDescriptorsCollectionProvider, decisionTreeProvider, actionConstraintProviders, loggerFactory)
        {
            _pluginManager = pluginManager;
        }

        protected override IReadOnlyList<ActionDescriptor> SelectBestActions(IReadOnlyList<ActionDescriptor> actions)
        {
            return FilterOnlyActiveActions(actions);
        }

        private List<ActionDescriptor> FilterOnlyActiveActions(IReadOnlyList<ActionDescriptor> actions)
        {

            if (actions == null)
                return null;
            var actionController = actions.Where(a => a is ControllerActionDescriptor).Cast<ControllerActionDescriptor>();
            if (actionController.Count() == 0)
                return null;
            var pluginNames = GetPluginForValidActionDescriptors(actionController).ToArray();
            var plugins = _pluginManager.Plugins.GetAll(a => pluginNames.Contains(a.Name)).ToArray();//Ver cuantas queries hace esto

            actionController.Select(a => new { Action = a, PluginName = _pluginManager.PluginAssemblyProvider.GetPluginContainingAssembly(a.ControllerTypeInfo.Assembly) });
            return actions?.Where(ActionsInActiveComponents).ToList();
        }

        private bool ActionsInActiveComponents(ActionDescriptor actionDescriptor)
        {
            var controllerActionDescriptor = ;
            if (controllerActionDescriptor != null)
            {
                var assembly = controllerActionDescriptor.ControllerTypeInfo.Assembly;
                var plugin = _pluginManager.GetPluginContainigAssembly(assembly);
                return plugin?.Enabled ?? true;
            }
            return true;
        }

        public IEnumerable<string> GetPluginForValidActionDescriptors(IEnumerable<ActionDescriptor> actions)
        {
            return actions.Where(a => a is ControllerActionDescriptor)
                .Cast<ControllerActionDescriptor>()
                .Select(a => _pluginManager.PluginAssemblyProvider.GetPluginContainingAssembly(a.ControllerTypeInfo.Assembly));
        }

    }
}
