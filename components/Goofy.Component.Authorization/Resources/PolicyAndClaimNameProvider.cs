using System;

namespace Goofy.Component.Authorization.Resources
{
    public class PolicyAndClaimNameProvider : IPolicyAndClaimNameProvider
    {
        public string GetCreateClaim(Type entityType)
        {
            return "CanCreate" + entityType.Name;
        }

        public string GetCreatePolicy(Type entityType)
        {
            return "RequireCreate" + entityType.Name;

        }

        public string GetDeleteClaim(Type entityType)
        {
            return "CanDelete" + entityType.Name;
        }

        public string GetDeletePolicy(Type entityType)
        {
            return "RequireDelete" + entityType.Name;
        }

        public string GetUpdateClaim(Type entityType)
        {
            return "CanUpdate" + entityType.Name;
        }

        public string GetUpdatePolicy(Type entityType)
        {
            return "RequireUpdate" + entityType.Name;
        }
    }
}
