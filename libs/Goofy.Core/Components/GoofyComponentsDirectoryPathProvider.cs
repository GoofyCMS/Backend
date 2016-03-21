using System.IO;
using Goofy.Core.Components.Base;

namespace Goofy.Core.Components
{
    public class GoofyComponentsDirectoryPathProvider : IComponentsDirectoryPathProvider
    {
        public string GetComponentsDirectoryPath()
        {
            return Directory.GetCurrentDirectory();
        }
    }
}
