using System;
using System.IO;
using System.Linq;

using Xunit;

using Goofy.Core.Components;
using Goofy.Core.Test.Mock;

namespace Goofy.Core.Test.Components
{
    public class GoofyInMemoryComponentStoreManagerTest
    {
        private class ComponentCreationFileContext : IDisposable
        {
            public ComponentCreationFileContext(string jsonFile)
            {
                var filePath = GetComponentFilePath();
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                using (var stream = new FileStream(filePath, FileMode.CreateNew))
                {
                    using (var fileWriter = new StreamWriter(stream))
                    {
                        fileWriter.Write(jsonFile);
                        fileWriter.Flush();
                    }
                }
            }

            public void Dispose()
            {
                var filePath = GetComponentFilePath();
                File.Delete(filePath);
            }

            private string GetComponentFilePath()
            {
                var mockComponentDirectoryPathProvider = new MockComponentDirectoryPathProvider();
                var filePath = Path.Combine(mockComponentDirectoryPathProvider.GetComponentsDirectoryPath(), "components.json");
                return filePath;
            }
        }
        private readonly GoofyInMemoryComponentStoreManager _sut;
        private readonly GoofyInMemoryComponentStore _componentStore;


        public GoofyInMemoryComponentStoreManagerTest()
        {
            _sut = new GoofyInMemoryComponentStoreManager(new MockComponentDirectoryPathProvider());
            _componentStore = new GoofyInMemoryComponentStore(_sut);
        }

        [Fact]
        public void ShouldLoadComponentStore()
        {
            var jsonFile = "[{\"ComponentId\":1,\"FullName\":\"Goofy.Component.Administration, Version = 1.0.0.0, Culture = neutral, PublicKeyToken = null\",\"Version\":\"1.0.0.0\",\"Installed\":true,\"IsSystemComponent\":false,\"GlobalId\":-9223372036854775808}]";
            using (var componentFileCreationContext = new ComponentCreationFileContext(jsonFile))
            {
                _sut.StartStore(_componentStore);
            }
            Assert.Equal(1, _componentStore.Components.Count());
            var component = _componentStore.Components.First();
            Assert.Equal(1, component.ComponentId);
            Assert.Equal("Goofy.Component.Administration, Version = 1.0.0.0, Culture = neutral, PublicKeyToken = null", component.FullName);
            Assert.Equal(-9223372036854775808, component.GlobalId);
            Assert.Equal(true, component.Installed);
            Assert.Equal(false, component.IsSystemComponent);
            Assert.Equal("1.0.0.0", component.Version);
        }

        [Fact]
        public void ShouldPersistComponentStore()
        {
            _componentStore.AddComponent(new Component { FullName = "component1", Installed = false });
            _componentStore.AddComponent(new Component { FullName = "component2", Installed = true });
            _componentStore.AddComponent(new Component { FullName = "component3", Installed = false });
            var anotherComponentStoreManager = new GoofyInMemoryComponentStoreManager(new MockComponentDirectoryPathProvider());
            var anotherComponentStore = new GoofyInMemoryComponentStore(anotherComponentStoreManager);
            anotherComponentStoreManager.StartStore(anotherComponentStore);
            Assert.Equal(3, anotherComponentStore.Components.Count());
        }
    }


}
