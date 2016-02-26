using Microsoft.Extensions.Configuration;

using Goofy.Core.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Goofy.WebFramework.Infrastructure
{
    public class GoofyWebEngine : GoofyEngine
    {
        private readonly IConfiguration _config;

        public GoofyWebEngine(IConfiguration config) : base()
        {
            _config = config;
        }

        public override void RegisterDependencies(IDependencyContainer dependencyContainer)
        {
            /* TODO: Agregar las configuraciones de cada clase...
               Tiene el problema es que la forma de llamar al método Configure es
               Configure<T> where T: class; lo que no admite coger el T dinámico de un ensamblado.
            */
            RegisterSortableDependencies<IGoofyDependencyAssembler>(dependencyContainer,
                                                        d =>
                                                            {
                                                                d.Register(dependencyContainer, ResourcesLoader);
                                                                d.RegisterWebDependencies((IWebDependencyContainer)dependencyContainer, ResourcesLoader, _config);
                                                            }
                                                    );
        }
    }
}
