using Microsoft.AspNet.Authorization;
using System.Security.Claims;

namespace Goofy.Security.Services
{
    public class AuthorizationService
    {
        private readonly AuthorizationContext _authorizationContext;
        private readonly CustomRequireClaimService _requireClaimService;

        public AuthorizationService(CustomRequireClaimService requireClaimService, AuthorizationContext authorizationContext)
        {
            _authorizationContext = authorizationContext;
            _requireClaimService = requireClaimService;
        }

        public bool IsAuthorized(string viewModelResource, string method)
        {
            var resource = GetResourceNameFromViewModelName(viewModelResource);
            var crudOperation = (method == "CREATE") ? CrudOperation.Create :
                                (method == "READ") ? CrudOperation.Read :
                                (method == "UPDATE") ? CrudOperation.Update :
                                 CrudOperation.Delete;
            var claim = new Claim(SecurityUtils.GetPermissionName(resource, crudOperation), "");
            return _requireClaimService.UserHasClaim(_authorizationContext.User, claim);
        }

        private static string GetResourceNameFromViewModelName(string viewModelName)
        {
            var name = viewModelName.Substring(viewModelName.LastIndexOf('.'));
            return name.Substring(0, name.LastIndexOf('I')); // Remove the "Item" sufix
        }

    }
}
