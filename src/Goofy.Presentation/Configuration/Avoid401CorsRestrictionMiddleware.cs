using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Cors.Infrastructure;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading.Tasks;

namespace Goofy.Presentation.Configuration
{
    public class Avoid401CorsRestrictionMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public Avoid401CorsRestrictionMiddleware(
            RequestDelegate next,
            ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<Avoid401CorsRestrictionMiddleware>();

        }

        public async Task Invoke(HttpContext context)
        {
            var corsPolicyProvider = context.ApplicationServices.GetRequiredService<ICorsPolicyProvider>();
            var corsService = context.ApplicationServices.GetRequiredService<ICorsService>();

            if (context.Request.Headers.ContainsKey(CorsConstants.Origin))
            {
                //IMPORTANT: THIS CODE IS FIXED TO "AllowNoOne" policy, it should be changed
                var corsPolicy = await corsPolicyProvider?.GetPolicyAsync(context, "AllowNoOne");
                if (corsPolicy != null)
                {
                    var accessControlRequestMethod =
                        context.Request.Headers[CorsConstants.AccessControlRequestMethod];
                    if (string.Equals(
                            context.Request.Method,
                            CorsConstants.PreflightHttpMethod,
                            StringComparison.Ordinal) &&
                            !StringValues.IsNullOrEmpty(accessControlRequestMethod))
                    {
                        // Since there is a policy which was identified,
                        // always respond to preflight requests.
                        var corsResult = corsService.EvaluatePolicy(context, corsPolicy);
                        corsService.ApplyResult(corsResult, context.Response);
                        context.Response.StatusCode = StatusCodes.Status204NoContent;
                        return;
                    }
                }
            }

            await _next(context);
        }
    }
}
