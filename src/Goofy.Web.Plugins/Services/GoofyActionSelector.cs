using System.Linq;
using System.Collections.Generic;

using Microsoft.AspNet.Mvc.ActionConstraints;
using Microsoft.AspNet.Mvc.Infrastructure;
using Microsoft.AspNet.Mvc.Routing;
using Microsoft.Extensions.Logging;
//using Microsoft.AspNet.Mvc.Controllers;
using Microsoft.AspNet.Mvc.Abstractions;

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
        //private readonly IComponentStore _componentStore;

        public GoofyActionSelector(
                                   IActionDescriptorsCollectionProvider actionDescriptorsCollectionProvider,
                                   IActionSelectorDecisionTreeProvider decisionTreeProvider,
                                   IEnumerable<IActionConstraintProvider> actionConstraintProviders,
                                   ILoggerFactory loggerFactory/*,*/
                                                               /*IComponentStore componentStore*/)
            : base(actionDescriptorsCollectionProvider, decisionTreeProvider, actionConstraintProviders, loggerFactory)
        {
            //_componentStore = componentStore;
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
            //var controllerActionDescriptor = actionDescriptor as ControllerActionDescriptor;
            //if (controllerActionDescriptor != null)
            //{
            //    var componentAssemblyName = controllerActionDescriptor.ControllerTypeInfo.Assembly.FullName;
            //    var componentInfo = _componentStore.Components.First(cI => cI.FullName == componentAssemblyName);
            //    if (componentInfo.Installed)
            //        return true;
            //    else
            //        return false;
            //}
            return true;
        }
    }
}
