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

        public IDictionary<string, IEnumerable<bool>> GetPermissions(ClaimsPrincipal user, string[] viewModelResources)
        {
            var result = new Dictionary<string, IEnumerable<bool>>();
            List<bool> permissions;
            foreach (var viewModel in viewModelResources)
            {
                permissions = new List<bool>();
                var resource = GetResourceNameFromViewModelName(viewModel);
                foreach (var operation in new[] { CrudOperation.Create, CrudOperation.Read, CrudOperation.Update, CrudOperation.Delete })
                {
                    var claim = new Claim(SecurityUtils.GetPermissionName(resource, operation), "");
                    permissions.Add(_requireClaimService.UserHasClaim(user, claim));
                }
                result.Add(viewModel, permissions);
            }
            return result;
        }


        private static string GetResourceNameFromViewModelName(string viewModelName)
        {
            return viewModelName.Substring(0, viewModelName.LastIndexOf('I')); // Remove the "Item" sufix
        }
    }
}
