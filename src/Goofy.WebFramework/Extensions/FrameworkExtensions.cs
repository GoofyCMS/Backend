using System.Linq;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using Goofy.Core.Configuration;
using Goofy.Core.Infrastructure;
using Goofy.Core.Components.Base;
using Goofy.Data;

using Goofy.WebFramework.Core.DependencyInjection;
using Goofy.WebFramework.Core.Extensions;
using Goofy.WebFramework.Data.DependencyInjection;
using Goofy.Core.DependencyInjection.DesignTimeExtensions;

namespace Goofy.WebFramework.Extensions
{
    public static class FrameworkExtensions
    {
        public static void AddGoofyWebFramework(this IServiceCollection services, IConfiguration config, ConfigurationBuilder configurationBuilder)
        {
            // Agregar las configuraciones básicas del framework
            services.Configure<GoofyCoreConfiguration>(config.GetSection(GoofyEngine.ConfigurationSection));
            services.Configure<GoofyDataConfiguration>(config.GetSection(GoofyDataAccessManager.ConfigurationSection));
            services.AddGoofyWeb(config, configurationBuilder);
            services.AddOptions();
        }

        private static void AddGoofyWeb(this IServiceCollection services, IConfiguration config, ConfigurationBuilder configurationBuilder)
        {
            //Crear el contenedor de dependencias que conoce Goofy
            services.AddGoofyCore();
            services.AddGoofyData();

            //Agregar los ficheros de configuración de las componentes al sistema de configuraciones
            var componentsInfoProvider = services.Resolve<IComponentsInfoProvider>();
            var componentsConfigFiles = componentsInfoProvider.ComponentsInfo.Select(compInfo => compInfo.ConfigFilePath);
            config = configurationBuilder.AddComponentsConfigurationFiles(componentsConfigFiles);

            var componentsAssembliesProvider = services.Resolve<IComponentsAssembliesProvider>();
            services.AddMvc().AddControllersAsServices(componentsAssembliesProvider.ComponentsAssemblies);

            var engine = services.Resolve<GoofyEngine>();
            engine.Start(services);
        }

        public static void AddGoofyCoreConfigurations(this ConfigurationBuilder builder)
        {
            builder.AddJsonFile(GoofyEngine.ConfigurationSource);
        }

        public static void AddGoofyDataConfigurations(this ConfigurationBuilder builder)
        {
            builder.AddJsonFile(GoofyDataAccessManager.ConfigurationSource);
        }

        /*
            Este método se va a pasar para métodos de extensión de 
        */
        //public static Type FindObjectContext(this System.Reflection.Assembly componentAssembly)
        //{
        //    return componentAssembly.GetExportedTypes().FirstOrDefault(t => t.IsSubclassOf(typeof(DbContext)));
        //}

    }
}
