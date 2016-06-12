using Microsoft.AspNet.Hosting;
using Goofy.Presentation.PluggableCore;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNet.Builder;
using Microsoft.Extensions.DependencyInjection;
using Goofy.Infrastructure.Core.Data;

namespace Goofy.Presentation
{
    public class Startup : GoofyPluginBasedStartup
    {
        private ConfigurationBuilder ConfigurationBuilder { get; set; }

        protected IConfiguration Configuration { get; set; }
        protected ILoggerFactory LoggerFactory { get; set; }

        public Startup(IHostingEnvironment env, IApplicationEnvironment app, ILoggerFactory loggerFactory)
        {
            LoggerFactory = loggerFactory;
            ConfigurationBuilder = new ConfigurationBuilder();
            ConfigurationBuilder.SetBasePath(app.ApplicationBasePath);
            ConfigurationBuilder.AddJsonFile("appsettings.json");
            Configuration = ConfigurationBuilder.Build();
            LoggerFactory.AddConsole(Configuration.GetSection("Logging"));
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DataAccessConfiguration>(Configuration.GetSection("DataAccessConfiguration"));
            base.ConfigureServices(services);
        }

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
