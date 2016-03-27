using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Logging;

namespace Goofy.Core.Infrastructure
{
    public class GoofyDomainResourcesLocator : AppDomainResourcesLocator
    {

        public GoofyDomainResourcesLocator(ILibraryManager libraryManager, ILogger<AppDomainResourcesLocator> logger)
            : base(libraryManager, logger)
        {
            BinFolderAlreadyLoaded = false;
        }

        private bool BinFolderAlreadyLoaded { get; set; }


        public override Assembly[] GetAssemblies()
        {
            //Conventions over configurations
            if (!BinFolderAlreadyLoaded)
            {
                BinFolderAlreadyLoaded = true;
                var binDirectoryPath = GetBinDirectoryPath();
                LoadBinFolder(binDirectoryPath);
            }
            _logger.LogInformation("GetAssemblies() method was called");
            int count = 1;
            foreach (var lib in _libraryManager.GetLibraries())
            {
                if (lib.Name.StartsWith("Goofy"))
                {
                    _logger.LogInformation("#{0}- LibraryName: {1}, LibraryType: {2}, LibraryVersion: {3}", count, lib.Name, lib.Type, lib.Version);
                    count += 1;
                }
            }
            return base.GetAssemblies().Where(a => ValidAssembly(a.FullName)).ToArray();
        }

        private void LoadBinFolder(string binDirectoryPath)
        {
            List<string> appDomainAssemblies = new List<string>();
            foreach (var assembly in base.GetAssemblies())
                appDomainAssemblies.Add(assembly.FullName);

            foreach (var dllPath in Directory.GetFiles(binDirectoryPath, "*.dll", SearchOption.AllDirectories))
            {
                var appDomain = AppDomain.CurrentDomain;
                try
                {
                    var assemblyName = AssemblyName.GetAssemblyName(dllPath);
                    if (ValidAssembly(assemblyName.FullName) && !appDomainAssemblies.Contains(assemblyName.FullName))
                    {
                        appDomain.Load(assemblyName);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public override string GetBinDirectoryPath()
        {
            var wwwDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
            var binDirectoryPath = string.Format("{0}\\artifacts\\bin", wwwDirectory.Parent.Parent.Parent.FullName);
            return binDirectoryPath;
        }

        private bool ValidAssembly(string assemblyName)
        {
            return true;
        }
    }
}
