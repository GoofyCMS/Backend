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
using Goofy.Presentation.Configuration;
using Goofy.Presentation.Configuration.Extensions;

namespace Goofy.Presentation
{
    public class Startup
    {
        private TokenValidationParameters _tokenValidationParameters;

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
            var secretKey = "mysupersecret_secretkey!123";
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            var options = new TokenProviderOptions
            {
                Audience = "GoofyAudience",
                Issuer = "GoofyIssuer",
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HMAC_SHA256),
            };
            services.Configure<TokenProviderOptions>(s =>
            {
                s.Audience = options.Audience;
                s.Issuer = options.Issuer;
                s.SigningCredentials = options.SigningCredentials;
            });
            _tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = options.Issuer,

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = options.Audience,

                // Validate the token expiry
                ValidateLifetime = true,

                ValidateSignature = false,

                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero
            };

            services.Configure<DataAccessConfiguration>(Configuration.GetSection("DataAccessConfiguration"));
            var corsConfigSection = Configuration.GetSection("CorsConfiguration");
            services.Configure<CorsConfiguration>(corsConfigSection);
            services.AddGoofySecurity();
            services.AddGoofy();
            var config = corsConfigSection.GetConfiguration<CorsConfiguration>();
            services.AddCorsPolicies(config);
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

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = _tokenValidationParameters
            });

            app.UseMiddleware<Avoid401CorsRestrictionMiddleware>();
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
