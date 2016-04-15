using System;
using System.Collections.Generic;

using Goofy.Component.Authorization.Model;

namespace Goofy.Component.Authorization.Resources
{
    public interface IGoofyClaimManager
    {
        IEnumerable<GoofyClaim> GetGoofyClaims();

        IEnumerable<GoofyClaim> GetGoofyCrudClaims();

        IEnumerable<GoofyClaim> GetGoofyNonCrudClaims();

        GoofyClaim AddCrudClaim(Type entityType, CrudOperation operation);

        GoofyClaim AddNonCrudClaim(string claimType, string claimValue);
    }
}
