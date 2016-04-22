using System.IO;
using System.Linq;
using System.Collections.Generic;

using Newtonsoft.Json;

using Goofy.Core.Components.Base;

namespace Goofy.Core.Components
{
    public class GoofyInMemoryComponentStoreManager : IComponentStoreStarter<GoofyInMemoryComponentStore>,
                                                      IComponentStorePersist<GoofyInMemoryComponentStore>
    {
        private const string componentFileName = "components.json";
        private readonly IComponentsDirectoryPathProvider _componentDirectoryPathProvider;

        public GoofyInMemoryComponentStoreManager(IComponentsDirectoryPathProvider componentDirectoryPathProvider)
        {
            _componentDirectoryPathProvider = componentDirectoryPathProvider;
        }

        public void StartStore(object store)
        {
            var storeStarter = (GoofyInMemoryComponentStore)store;
            StartStoreHelp(storeStarter);
        }

        private void StartStoreHelp(GoofyInMemoryComponentStore emptyStore)
        {
            var componentsFolder = _componentDirectoryPathProvider.GetComponentsDirectoryPath();
            var filePath = Path.Combine(componentsFolder, componentFileName);
            if (File.Exists(filePath))
            {
                try
                {
                    List<Component> components;
                    using (var fileStream = new FileStream(filePath, FileMode.Open))
                    {
                        using (var streamReader = new StreamReader(fileStream))
                        {
                            var serializer = new JsonSerializer();
                            components = (List<Component>)serializer.Deserialize(streamReader, typeof(List<Component>));
                        }
                    }
                    foreach (var component in components)
                    {
                        emptyStore.AddComponent(component);
                    }
                }
                catch { }

            }
        }

        public void PersistComponentStore(GoofyInMemoryComponentStore store)
        {
            var componentsFolder = _componentDirectoryPathProvider.GetComponentsDirectoryPath();
            var filePath = Path.Combine(componentsFolder, componentFileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.CreateNew))
                {
                    using (var streamWriter = new StreamWriter(fileStream))
                    {
                        var serializer = new JsonSerializer();
                        serializer.Serialize(streamWriter, store.Components.ToList());
                    }
                }
            }
            catch { }

        }
    }
}
