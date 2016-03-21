using System;
using System.Reflection;

namespace Goofy.Core.Infrastructure
{
    public class AppDomainResourcesLocator : IResourcesLocator
    {
        public virtual Assembly[] GetAssemblies()
        {
            //var reflectinOnlyAssemblies = AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies();
            //string a = reflectinOnlyAssemblies.ToString();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            return assemblies;
        }

        public virtual string GetBinDirectoryPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}
