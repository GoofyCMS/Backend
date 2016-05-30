using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Builder;

using Goofy.Application.Core.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;

namespace Goofy.Presentation.Core
{
    public class GoofyStartup
    {
        private ConfigurationBuilder ConfigurationBuilder { get; set; }

        protected IConfiguration Configuration { get; set; }
        protected ILoggerFactory LoggerFactory { get; set; }

        public GoofyStartup(IHostingEnvironment env, IApplicationEnvironment app, ILoggerFactory loggerFactory)
        {
            LoggerFactory = loggerFactory;
            ConfigurationBuilder = new ConfigurationBuilder();
            ConfigurationBuilder.SetBasePath(app.ApplicationBasePath);
            ConfigurationBuilder.AddJsonFile("appsettings.json");
            Configuration = ConfigurationBuilder.Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddGoofyCore();//agregar las dependencias del Framework Goofy
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
