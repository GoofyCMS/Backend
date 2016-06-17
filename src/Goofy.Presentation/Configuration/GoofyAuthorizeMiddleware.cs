using Goofy.Security.Services;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Goofy.Presentation.Configuration
{
    internal class GoofyAuthorizeMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        private readonly GoofyAuthorizeOptions _options;
        private readonly JsonSerializerSettings _serializerSettings;

        public GoofyAuthorizeMiddleware(
           RequestDelegate next,
           GoofyAuthorizeOptions options,
           ILoggerFactory loggerFactory)
        {
            _next = next;
            _options = options;
            _logger = loggerFactory.CreateLogger<TokenProviderMiddleware>();

            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }

        public Task Invoke(HttpContext context)
        {
            // If the request path doesn't match, skip
            if (!context.Request.Path.Equals(_options.Path, StringComparison.Ordinal))
            {
                return _next(context);
            }

            // Request must be POST with Content-Type: application/x-www-form-urlencoded
            if (!context.Request.Method.Equals("POST")
               || !context.Request.HasFormContentType)
            {
                context.Response.StatusCode = 400;
                return context.Response.WriteAsync("Bad request.");
            }

            _logger.LogInformation("Handling request: " + context.Request.Path);

            return IsAuthorized(context);
        }

        private async Task IsAuthorized(HttpContext context)
        {
            var viewModel = context.Request.Form[_options.ResourceField];
            if (viewModel == "")
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync($"Fields \"{_options.ResourceField}\" and \"{_options.MethodField}\" are required.");
                return;
            }
            var authorizationService = context.ApplicationServices.GetRequiredService<AuthorizationService>();
            var permissions = authorizationService.GetPermissions(context.User, viewModel);

            var permissionResponse = new
            {
                permissions = permissions.ToArray()
            };

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(permissionResponse, _serializerSettings));
        }
    }
}