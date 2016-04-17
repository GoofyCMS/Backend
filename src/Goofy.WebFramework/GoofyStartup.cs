using System.IO;

using Goofy.Extensions;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;

namespace Goofy.WebFramework
{
    public class GoofyStartup
    {
        private ConfigurationBuilder ConfigurationBuilder { get; set; }

        private IConfiguration Configuration { get; set; }

        public GoofyStartup(IHostingEnvironment env, IApplicationEnvironment app)
        {
            /* 
                TODO:
                Esto debería poderse hacer desde configuraciónes pero, no está funcionando
                el atributo "webroot" en el project.json.
                **Buscar como mejorar esto**
            */
            Directory.SetCurrentDirectory(string.Format("{0}\\wwwroot", app.ApplicationBasePath));
            ConfigurationBuilder = new ConfigurationBuilder();
            ConfigurationBuilder.SetBasePath(string.Format("{0}\\bin", app.ApplicationBasePath));
            ConfigurationBuilder.AddJsonFile("appsettings.json");
            Configuration = ConfigurationBuilder.Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            var loggerFactory = services.Resolve<ILoggerFactory>();
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            services.AddGoofyWebFramework();//agregar las dependencias del Framework Goofy
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddDebug();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseRuntimeInfoPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                try
                {
                    //using (var serviceScope = app.ApplicationServices.GetRequiredService < IServiceScop.CreateScope())
                    //{
                    //    serviceScope.ServiceProvider.GetService<ApplicationDbContext>()
                    //    .Database.Migrate();
                    //}
                }
                catch { }
            }

            /*
                Está cableado, se podría buscar una mejor forma de configurarlo, usando la componente
                Goofy.Component.Auth
            */
            app.UseIdentity();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
