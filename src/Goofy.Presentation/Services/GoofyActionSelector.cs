using Goofy.Application.Administration.Extensions;
using Goofy.Application.Administration.Services.Abstractions;
using Microsoft.AspNet.Mvc.Abstractions;
using Microsoft.AspNet.Mvc.ActionConstraints;
using Microsoft.AspNet.Mvc.Controllers;
using Microsoft.AspNet.Mvc.Infrastructure;
using Microsoft.AspNet.Mvc.Routing;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

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
            return actions?.Where(ActionsInActiveComponents).ToList();
        }

        private bool ActionsInActiveComponents(ActionDescriptor actionDescriptor)
        {
            var controllerActionDescriptor = actionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor != null)
            {
                var assembly = controllerActionDescriptor.ControllerTypeInfo.Assembly;
                var plugin = _pluginManager.GetPluginContainigAssembly(assembly);
                return plugin?.Enabled ?? true;
            }
            return true;
        }
    }
}
