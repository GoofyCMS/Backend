using System;
using Xunit;
using System.Collections.Generic;
using System.Reflection;
using Goofy.Core.Infrastructure;

namespace Goofy.Core.Test.Infrastructure.ResourcesManagment
{
    public class GoofyResourcesLocatorTest
    {
        private class MockAssembliesProvider : IAssembliesProvider
        {
            private readonly Assembly[] _assemblies;

            public MockAssembliesProvider(params Assembly[] assemblies)
            {
                _assemblies = assemblies;
            }
            public IEnumerable<Assembly> GetAssemblies()
            {
                return _assemblies;
            }
        }

        GoofyResourcesLocator locator;
        
        /*
            Testear:
                - Que no se resuelvan clases abstractas
                - Que no se resuelvan interfaces
                - Que se resuelvan todas las dependencias en 1 y en múltiples ensamblados
        */
    }


}
