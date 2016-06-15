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
using System.IdentityModel.Tokens;
using System.Text;
using System;
using Microsoft.AspNet.Authentication.JwtBearer;
using Microsoft.Extensions.OptionsModel;
using Microsoft.AspNet.Identity;
using Goofy.Domain.Identity.Entity;

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

            if (env.IsDevelopment())
            {
                loggerFactory.AddDebug();
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
            var secretKey = "mysupersecret_secretkey!123";
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            //var tokenValidationParameters = new TokenValidationParameters
            //{
            //    // The signing key must match!
            //    ValidateIssuerSigningKey = true,
            //    IssuerSigningKey = signingKey,

            //    // Validate the JWT Issuer (iss) claim
            //    ValidateIssuer = true,
            //    ValidIssuer = "ExampleIssuer",

            //    // Validate the JWT Audience (aud) claim
            //    ValidateAudience = true,
            //    ValidAudience = "ExampleAudience",

            //    // Validate the token expiry
            //    ValidateLifetime = true,

            //    // If you want to allow a certain amount of clock drift, set that here:
            //    ClockSkew = TimeSpan.Zero
            //};
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");
            });

            var options = new TokenProviderOptions
            {
                Audience = "ExampleAudience",
                Issuer = "ExampleIssuer",
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HMAC_SHA256),
            };
            app.UseMiddleware<TokenProviderMiddleware>(options);
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
