using System;
using System.Collections.Generic;
using Goofy.Component.Authorization.Model;

namespace Goofy.Component.Authorization.Resources
{
    public class GoofyCrudPoliciesManager : IGoofyCrudPoliciesManager
    {
        private readonly List<GoofyCrudPolicy> _goofyPolicies;
        private readonly IGoofyClaimManager _claimManager;
        private readonly PolicyNameBuilder _policyNameBuilder;

        public GoofyCrudPoliciesManager(IGoofyClaimManager claimManager)
        {
            _goofyPolicies = new List<GoofyCrudPolicy>();
            _policyNameBuilder = new PolicyNameBuilder();
            _claimManager = claimManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="operation"></param>
        /// <returns>
        ///  A new GoofyCrudPolicy if it didn't exist before, otherwise null;
        /// </returns>
        public GoofyCrudPolicy AddNewGoofyCrudPolicy(Type entityType, CrudOperation operation)
        {
            var claim = _claimManager.AddCrudClaim(entityType, operation);
            if (claim != null)
            {
                var policyName = _policyNameBuilder.GetPolicyName(entityType, operation);
                var policy = new GoofyCrudPolicy(policyName, claim.ClaimType);
                _goofyPolicies.Add(policy);
                return policy;
            }
            return null;
        }

        public IEnumerable<GoofyCrudPolicy> GetGoofyPolicies()
        {
            return _goofyPolicies;
        }
    }
}
