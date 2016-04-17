using System.Linq;
using System.Collections.Generic;

using Microsoft.AspNet.Mvc.ActionConstraints;
using Microsoft.AspNet.Mvc.Infrastructure;
using Microsoft.AspNet.Mvc.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.AspNet.Mvc.Controllers;
using Microsoft.AspNet.Mvc.Abstractions;

using Goofy.Core.Components.Base;

namespace Microsoft.AspNet.Mvc
{
    public class GoofyActionSelector : DefaultActionSelector
    {
        private readonly IComponentStore _componentStore;

        public GoofyActionSelector(
                                   IActionDescriptorsCollectionProvider actionDescriptorsCollectionProvider,
                                   IActionSelectorDecisionTreeProvider decisionTreeProvider,
                                   IEnumerable<IActionConstraintProvider> actionConstraintProviders,
                                   ILoggerFactory loggerFactory,
                                   IComponentStore componentStore)
            : base(actionDescriptorsCollectionProvider, decisionTreeProvider, actionConstraintProviders, loggerFactory)
        {
            _componentStore = componentStore;
        }

        protected override IReadOnlyList<ActionDescriptor> SelectBestActions(IReadOnlyList<ActionDescriptor> actions)
        {
            return FilterOnlyActiveActions(actions);
        }

        private List<ActionDescriptor> FilterOnlyActiveActions(IReadOnlyList<ActionDescriptor> actions)
        {
            return actions.Where(ActionsInActiveComponents).ToList();
        }

        private bool ActionsInActiveComponents(ActionDescriptor actionDescriptor)
        {
            var controllerActionDescriptor = actionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor != null)
            {
                var componentAssemblyName = controllerActionDescriptor.ControllerTypeInfo.Assembly.FullName;
                var componentInfo = _componentStore.Components.First(cI => cI.FullName == componentAssemblyName);
                if (componentInfo.Installed)
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
            return false;
        }
    }
}
