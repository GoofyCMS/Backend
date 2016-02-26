using System;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.Entity;

using Goofy.Core.Configuration;
using Goofy.Core.Infrastructure;
using Goofy.Core.Components;
using Goofy.Data;
using Microsoft.Data.Entity.Infrastructure;

using Goofy.WebFramework.Infrastructure;
using Goofy.WebFramework.Components;
using Microsoft.Data.Entity.Migrations.Design;


namespace Goofy.WebFramework
{
    public static class FrameworkExtensions
    {
        public static void AddGoofy(this IServiceCollection services, IConfiguration config, ConfigurationBuilder configurationBuilder)
        {
            // Agregar las configuraciones básicas del framework
            services.Configure<GoofyCoreConfiguration>(config.GetSection(GoofyEngine.ConfigurationSection));
            services.Configure<GoofyDataConfiguration>(config.GetSection(GoofyDataAccessManager.ConfigurationSection));
            services.AddGoofyCore(config, configurationBuilder);
        }

        private static void AddGoofyCore(this IServiceCollection services, IConfiguration config, ConfigurationBuilder configurationBuilder)
        {
            //Obtener el objeto que permite registrar los objetos DbContext de Entity Framework
            var builder = services.AddEntityFramework();

            //Crear el contenedor de dependencias que conoce Goofy
            var dependencyContainer = new AspNetMvcContainer(services, builder);
            builder.AddGoofyData(dependencyContainer);

            //Agregar las componentes de 3ros a la aplicación y decirle a Mvc de donde tiene que cargar los controladores
            var componentManager = new GoofyWebComponentManager(dependencyContainer);
            //Esta operación podría hacerse en un método extensor
            foreach (var jsonConfigFilePath in componentManager.ValidComponentsJson)
                configurationBuilder.AddJsonFile(jsonConfigFilePath);

            config = configurationBuilder.Build();
            services.AddInstance<GoofyComponentManager>(componentManager);
            componentManager.LoadComponentsFromAssemblies(dependencyContainer);

            //Esta operación podría hacerse un método extensor
            services.AddMvc().AddControllersAsServices(componentManager.ComponentAssemblies);

            var engine = new GoofyWebEngine(config);
            engine.Start(dependencyContainer);
            services.AddInstance<IEngine>(engine);
            services.AddInstance<IDependencyContainer>(dependencyContainer);
        }

        private static void AddGoofyData(this EntityFrameworkServicesBuilder configurationBuilder, IDependencyContainer dependencyContainer)
        {
            /*
                La línea de abajo está comentada, porque aunque es lo sugerido en la documentación oficial:
                    - Hacer configurationBuilder.AddSqlServer(connectionString)
                Cuando se hace, no me funciona la resolución de algunas dependencias de EntityFramework.
            */
            GoofyDataAccessManager.GoofyDataConfiguration.Provider.AddDataProvider(configurationBuilder);
            GoofyDataAccessManager.GoofyDataConfiguration.Provider.AddRelationalAnnotationProvider(dependencyContainer);
            GoofyDataAccessManager.GoofyDataConfiguration.Provider.AddMigrationsAnnotationProvider(dependencyContainer);
            GoofyDataAccessManager.GoofyDataConfiguration.Provider.AddSqlCommandBuilderAndDependencies(dependencyContainer);
            GoofyDataAccessManager.GoofyDataConfiguration.Provider.AddDbContextObject<ComponentContext>(configurationBuilder);
        }

        private static void AddMigrationsStuff(this ServiceCollection services)
        {
            services.AddSingleton<CSharpHelper>();
            services.AddSingleton<CSharpMigrationOperationGenerator>();
            services.AddSingleton<CSharpSnapshotGenerator>();
        }

        public static void AddGoofyCoreConfigurations(this ConfigurationBuilder builder)
        {
            builder.AddJsonFile(GoofyEngine.ConfigurationSource);
        }

        public static void AddGoofyDataConfigurations(this ConfigurationBuilder builder)
        {
            builder.AddJsonFile(GoofyDataAccessManager.ConfigurationSource);
        }

        public static Type FindObjectContext(this System.Reflection.Assembly componentAssembly)
        {
            return componentAssembly.GetExportedTypes().FirstOrDefault(t => t.IsSubclassOf(typeof(DbContext)));
        }

    }
}
