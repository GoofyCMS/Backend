using Goofy.Application.Core;
using Goofy.Application.Core.Abstractions;
using Goofy.Application.Core.Extensions;
using Goofy.Domain.Core;
using Goofy.Presentation.Core.Providers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Goofy.Presentation.Core
{
    public class ContextProviderAdder : IDependencyRegistrar
    {
        public void ConfigureServices(IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            foreach (var contextProviderType in FindContextProvider(assemblies.GetAssembliesPerLayer(AppLayer.Presentation)))
            {
                services.AddScoped(contextProviderType);
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
