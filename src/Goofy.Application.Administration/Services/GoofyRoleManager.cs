using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Goofy.Application.Administration.Services
{
    public class GoofyRoleManager<TRole> : RoleManager<TRole> where TRole : class
    {
        public GoofyRoleManager(IRoleStore<TRole> store,
                                IEnumerable<IRoleValidator<TRole>> roleValidators,
                                ILookupNormalizer keyNormalizer,
                                IdentityErrorDescriber errors,
                                ILogger<RoleManager<TRole>> logger,
                                IHttpContextAccessor contextAccessor)
            : base(store, roleValidators, keyNormalizer, errors, logger, contextAccessor)
        { }
    }
}
