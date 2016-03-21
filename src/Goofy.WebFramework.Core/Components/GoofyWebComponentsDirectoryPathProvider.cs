using System;
using System.IO;
using Goofy.Core.Components.Base;

namespace Goofy.WebFramework.Core.Components
{
    public class GoofyWebComponentsDirectoryPathProvider : IComponentsDirectoryPathProvider
    {
        protected readonly string ComponentDirectoryName = "components";

        public string GetComponentsDirectoryPath()
        {
            var wwwDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
            var componentsDirectoryPath = string.Format("{0}\\{1}", wwwDirectory.Parent.FullName, ComponentDirectoryName);
            return componentsDirectoryPath;
        }
    }
}
