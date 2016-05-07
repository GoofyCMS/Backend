using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Goofy.Domain.Core.Service.Adapter;
using Goofy.Domain.Core.Abstractions;

namespace Goofy.Infrastructure.Core.Adapters
{
    public class AutomapperTypeAdapterFactory : ITypeAdapterFactory, IDisposable
    {
        //private bool ite;

        protected MapperConfiguration Config { get; }

        #region Constructor

        /// <summary>
        ///     Create a new Automapper type adapter factory
        /// </summary>
        public AutomapperTypeAdapterFactory(IPluginAssemblyProvider pluginAssemblies)
        {
            //scan all assemblies finding Automapper Profile
            var profiles = pluginAssemblies.GetAssemblies
                                           .FindClassesOfType<Profile>()
                                           .Where(item => !item.FullName.StartsWith("AutoMapper"));

            Config = new MapperConfiguration(cfg =>
            {
                foreach (var item in profiles)
                {
                    cfg.AddProfile(Activator.CreateInstance(item) as Profile);
                }
            });
            Config.AssertConfigurationIsValid();
        }

        #endregion

        #region ITypeAdapterFactory Members

        public ITypeAdapter Create()
        {
            return new AutomapperTypeAdapter(Config);
        }

        public IEnumerable<Type> GetSourceTypes(Type targetType)
        {
            return Config.GetAllTypeMaps().Where(e => e.DestinationType == targetType).Select(e => e.SourceType);
        }

        public IEnumerable<Type> GetTargetTypes(Type sourceType)
        {
            return Config.GetAllTypeMaps().Where(e => e.SourceType == sourceType).Select(e => e.DestinationType);
        }

        #endregion

        public void Dispose()
        {
            // nothing for now
        }
    }
}