using Microsoft.AspNet.Cors.Infrastructure;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.OptionsModel;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Goofy.Presentation.Configuration
{
    public class CustomCorsRequestValidator
    {
        private readonly CorsConfiguration _corsConfig;

        public CustomCorsRequestValidator(IOptions<CorsConfiguration> corsConfig)
        {
            _corsConfig = corsConfig.Value;
        }

        public async Task<bool> AuthorizeCors(HttpContext context)
        {
            var corsPolicyProvider = context.ApplicationServices.GetRequiredService<ICorsPolicyProvider>();
            var corsService = context.ApplicationServices.GetRequiredService<ICorsService>();

            if (context.Request.Headers.ContainsKey(CorsConstants.Origin))
            {
                foreach (var policyName in _corsConfig.Policies.Select(p => p.Name))
                {
                    var corsPolicy = await corsPolicyProvider?.GetPolicyAsync(context, policyName);
                    if (corsPolicy != null)
                    {
                        var corsResult = corsService.EvaluatePolicy(context, corsPolicy);
                        corsService.ApplyResult(corsResult, context.Response);

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
                            context.Response.StatusCode = StatusCodes.Status204NoContent;
                            return await Task.FromResult(true);
                        }
                    }
                }
            }
            return await Task.FromResult(false);
        }
    }
}
