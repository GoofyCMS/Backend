using Goofy.Core.Components.Base;
using System.IO;

namespace Goofy.Core.Test.Mock
{
    public class MockComponentDirectoryPathProvider : IComponentsDirectoryPathProvider
    {
        public string GetComponentsDirectoryPath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "_componentsStuff");
        }
    }
}
