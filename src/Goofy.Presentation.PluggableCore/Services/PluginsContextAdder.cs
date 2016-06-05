using Goofy.Application.PluggableCore.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Goofy.Presentation.Core.Providers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Goofy.Application.PluggableCore.Extensions;

namespace Goofy.Presentation.PluggableCore.Services
{
    public class PluginsContextAdder : PluginDependenciesAdder
    {
        public override void AddPluginExtraDependencies(IServiceCollection services, IPluginManager manager)
        {
            foreach (var contextProviderType in FindContextProvider(manager.GetAssembliesPerLayer(AppLayer.Presentation)))
            {
                services.AddSingleton(contextProviderType);
            }
        }

        private IEnumerable<Type> FindContextProvider(IEnumerable<Assembly> assemblies)
        {
            foreach (var type in assemblies.SelectMany(ass => ass.GetExportedTypes()))
            {
                var typeInfo = type.GetTypeInfo();
                if (typeInfo.BaseType.GenericTypeArguments.Length == 1)
                {
                    var genericDefinition = typeof(BaseContextProvider<>).MakeGenericType(typeInfo.BaseType.GenericTypeArguments);
                    if (type.IsClassOfType(genericDefinition))
                        yield return type;
                }
            }
        }
    }
}
