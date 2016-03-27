using System.IO;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Goofy.WebApi
{
    public class Startup
    {
        private ConfigurationBuilder ConfigurationBuilder { get; set; }

        private IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env, IApplicationEnvironment app)
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
            services.AddGoofyWebFramework();//agregar las dependencias del Framework Goofy
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
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

            app.UseIISPlatformHandler();

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

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
