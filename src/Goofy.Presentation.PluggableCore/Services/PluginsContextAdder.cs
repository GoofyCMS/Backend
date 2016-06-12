using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Goofy.Domain.Core;
using Goofy.Application.PluggableCore.Abstractions;
using Goofy.Application.PluggableCore.Extensions;
using Goofy.Presentation.Core.Providers;

namespace Goofy.Presentation.PluggableCore.Services
{
    public class PluginsContextAdder : IDependencyRegistrar
    {
        public void ConfigureServices(IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            foreach (var contextProviderType in FindContextProvider(assemblies.GetAssembliesPerLayer(AppLayer.Presentation)))
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
