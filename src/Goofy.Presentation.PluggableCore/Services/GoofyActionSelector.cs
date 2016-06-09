using System.Linq;
using System.Collections.Generic;

using Microsoft.AspNet.Mvc.ActionConstraints;
using Microsoft.AspNet.Mvc.Infrastructure;
using Microsoft.AspNet.Mvc.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.AspNet.Mvc.Abstractions;
using Microsoft.AspNet.Mvc.Controllers;
using Goofy.Application.PluggableCore.Abstractions;
using Goofy.Application.PluggableCore.Extensions;

//using Goofy.Core.Components.Base;

namespace Microsoft.AspNet.Mvc
{
    /*
        TODO: Agregar comentarios a la clase y testear que está pinchando el GoofyActionSelector, 
        2 tests mínimo son requeridos
            - Comprobar que se retorna 404 cuando una ruta de un controlador que pertenece a una 
            componente que no está instalada
            - Comprobar que la acción se ejecuta correctamente cuando la ruta solicitada pertenece a
            un controlador que pertenece a una componente que está instalada.
    */
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
