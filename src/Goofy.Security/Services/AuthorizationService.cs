using Microsoft.AspNet.Authorization;
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

        private const string CREATE = "CREATE";
        private const string READ = "READ";
        private const string UPDATE = "UPDATE";
        private const string DELETE = "DELETE";

        public bool IsAuthorized(ClaimsPrincipal user, string viewModelResource, string operation)
        {
            var resource = GetResourceNameFromViewModelName(viewModelResource);
            if (!Validate(operation))
                return false;
            var crudOperation = (operation == CREATE) ? CrudOperation.Create :
                                (operation == READ) ? CrudOperation.Read :
                                (operation == UPDATE) ? CrudOperation.Update :
                                CrudOperation.Delete;

            var claim = new Claim(SecurityUtils.GetPermissionName(resource, crudOperation), "");
            return _requireClaimService.UserHasClaim(user, claim);
        }

        private bool Validate(string method)
        {
            return method == CREATE || method == READ || method == UPDATE || method == DELETE;
        }

        private static string GetResourceNameFromViewModelName(string viewModelName)
        {
            //No validatios are made here
            var name = viewModelName.Substring(viewModelName.LastIndexOf('.') + 1);
            return name.Substring(0, name.LastIndexOf('I')); // Remove the "Item" sufix
        }

    }
}
