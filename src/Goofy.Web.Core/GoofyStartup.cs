using System;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Builder;

using Goofy.Application.Core.Extensions;
using Goofy.Application.Core.DependencyInjection;

namespace Goofy.Web.Core
{
    public class GoofyStartup
    {
        private ConfigurationBuilder ConfigurationBuilder { get; set; }

        private IConfiguration Configuration { get; set; }

        public GoofyStartup(IHostingEnvironment env, IApplicationEnvironment app)
        {
            //Directory.SetCurrentDirectory(string.Format("{0}\\wwwroot", app.ApplicationBasePath));
            ConfigurationBuilder = new ConfigurationBuilder();
            ConfigurationBuilder.SetBasePath(string.Format("{0}\\bin", app.ApplicationBasePath));
            //ConfigurationBuilder.AddJsonFile("appsettings.json");
            Configuration = ConfigurationBuilder.Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            /* TODO:
                Buscar la forma de resolver el ILoggerFactory para configurarlo desde que se
                carga el framework a agregar uno para tiempo de carga del framework diferente
            */
            //var loggerFactory = services.Resolve<ILoggerFactory>();
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            services.AddGoofyCore();//agregar las dependencias del Framework Goofy
            return services.ConfigureServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
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
            //app.UseIdentity();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //    name: "default",
            //    template: "{controller=Home}/{action=Index}/{id?}");
            //});
        }
    }
}
