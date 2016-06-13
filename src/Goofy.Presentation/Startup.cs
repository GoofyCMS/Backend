using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNet.Builder;
using Microsoft.Extensions.DependencyInjection;
using Goofy.Infrastructure.Core.Data;
using Goofy.Security.DependencyInjection;
using Goofy.Application.DependencyInjection;
using Goofy.Presentation.DependencyInjection;

namespace Goofy.Presentation
{
    public class Startup
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

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DataAccessConfiguration>(Configuration.GetSection("DataAccessConfiguration"));
            services.AddGoofySecurity();
            services.AddGoofy();
            services.AddMvcServices();
            services.StartEngine();
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
