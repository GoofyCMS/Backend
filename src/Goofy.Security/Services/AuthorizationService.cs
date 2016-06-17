using System.Collections.Generic;
using System.Security.Claims;

namespace Goofy.Security.Services
{
    public class AuthorizationService
    {
        private readonly CustomRequireClaimService _requireClaimService;

        public AuthorizationService(CustomRequireClaimService requireClaimService)
        {
            _requireClaimService = requireClaimService;
        }

        public IEnumerable<bool> GetPermissions(ClaimsPrincipal user, string viewModelResource)
        {
            var resource = GetResourceNameFromViewModelName(viewModelResource);

            foreach (var operation in new[] { CrudOperation.Create, CrudOperation.Read, CrudOperation.Update, CrudOperation.Delete })
            {
                var claim = new Claim(SecurityUtils.GetPermissionName(resource, operation), "");
                yield return _requireClaimService.UserHasClaim(user, claim);
            }
        }


        private static string GetResourceNameFromViewModelName(string viewModelName)
        {
            //No validatios are made here
            var name = viewModelName.Substring(viewModelName.LastIndexOf('.') + 1);
            return name.Substring(0, name.LastIndexOf('I')); // Remove the "Item" sufix
        }

    }
}
