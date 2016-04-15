using System;
using System.Collections.Generic;

using Goofy.Component.Authorization.Model;

namespace Goofy.Component.Authorization.Resources
{
    public interface IGoofyCrudPoliciesManager
    {
        IEnumerable<GoofyCrudPolicy> GetGoofyPolicies();

        GoofyCrudPolicy AddNewGoofyCrudPolicy(Type entityType, CrudOperation operation);
    }
}
