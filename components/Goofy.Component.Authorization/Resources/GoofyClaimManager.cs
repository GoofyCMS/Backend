using System;
using System.Linq;
using System.Collections.Generic;

using Goofy.Component.Authorization.Model;

namespace Goofy.Component.Authorization.Resources
{
    public class GoofyClaimManager : IGoofyClaimManager
    {
        private readonly HashSet<GoofyClaim> _claims;
        private readonly ClaimNameBuilder _claimNameBuilder;

        public GoofyClaimManager()
        {
            _claims = new HashSet<GoofyClaim>();
            _claimNameBuilder = new ClaimNameBuilder();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityType">
        /// GoofyEntityBase class
        /// </param>
        /// <param name="operation"></param>
        /// <returns>
        /// A new GoofyClaim if it didn't exist before, otherwise null;
        /// </returns>
        public GoofyClaim AddCrudClaim(Type entityType, CrudOperation operation)
        {
            var claimType = _claimNameBuilder.GetClaimType(entityType, operation);
            var crudClaim = new GoofyClaim(claimType, null, true);
            if (_claims.Contains(crudClaim))
                return null;
            _claims.Add(crudClaim);
            return crudClaim;
        }

        public GoofyClaim AddNonCrudClaim(string claimType, string claimValue)
        {
            var claim = new GoofyClaim(claimType, claimValue, false);
            if (_claims.Contains(claim))
            {
                return null;
            }
            _claims.Add(claim);
            return claim;
        }

        public IEnumerable<GoofyClaim> GetGoofyClaims()
        {
            return _claims;
        }

        public IEnumerable<GoofyClaim> GetGoofyCrudClaims()
        {
            return _claims.Where(c => c.IsCrudClaim);
        }

        public IEnumerable<GoofyClaim> GetGoofyNonCrudClaims()
        {
            return _claims.Where(c => !c.IsCrudClaim);
        }
    }
}
